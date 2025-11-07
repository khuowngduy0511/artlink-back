using Application.AppConfigurations;
using Application.Commons;
using Application.Models.ZaloPayModels;
using Application.Services.Abstractions;
using AutoMapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Application.Services;

public class ZaloPayService : IZaloPayService
{
    private readonly string ZalopayBaseUrl;
    private const string CreateOrderUrl = "/v2/create";
    private const string QueryOrderUrl = "/v2/query";
    private const string QueryUserUrl = "/v2/disbursement/user";
    private const string TopupUrl = "/v2/disbursement/topup";
    private const string QueryMerchantBalanceUrl = "/v2/disbursement/balance";

    private const string CallbackUrl = "/api/Payments/callback";

    private readonly ZaloPayConfiguration _zaloPayConfig;
    private readonly HttpClient _httpClient;
    private readonly IMapper _mapper;
    private readonly IClaimService _claimService;

    public ZaloPayService(AppConfiguration appConfiguration, IMapper mapper,
        IClaimService claimService)
    {
        _zaloPayConfig = appConfiguration?.ZaloPayConfiguration ?? throw new ArgumentNullException(nameof(appConfiguration), "ZaloPayConfiguration is not configured in appsettings.json");
        _httpClient = new HttpClient();
        _mapper = mapper;
        ZalopayBaseUrl = _zaloPayConfig.BaseUrl ?? throw new ArgumentNullException(nameof(_zaloPayConfig.BaseUrl), "ZaloPay BaseUrl is not configured");
        _claimService = claimService;
    }

    public ZPCreateOrderRequest BuildZaloPayOrderCreate(OrderCreateModel model)
    {
        var appTime = CurrentTime.GetTimeStamp();
        var appUser = _claimService.GetCurrentUserId.ToString();
        appUser = appUser != null ? appUser : "AnonymousUser";
        var rnd = new Random();
        var appTransId = CurrentTime.GetCurrentTime.ToString("yyMMdd") + "_" + rnd.Next(1000000);
        var data = $"{_zaloPayConfig.AppId}|{appTransId}|{appUser}|{model.Amount}|{appTime}|{model.EmbedData}|{model.Item}";

        var zalopayOrder = _mapper.Map<ZPCreateOrderRequest>(model);
        zalopayOrder.AppId = _zaloPayConfig.AppId;
        zalopayOrder.AppUser = appUser;
        zalopayOrder.AppTransId = appTransId;
        zalopayOrder.AppTime = appTime;
        zalopayOrder.Description = "Nap xu nen tang ArtLink.";
        zalopayOrder.CallbackUrl = "http://localhost:8080" + CallbackUrl;
        zalopayOrder.Mac = CryptoHelper.HMacCompute(_zaloPayConfig.Key1, data);

        return zalopayOrder;
    }

    public async Task<ZPCreateOrderResponse?> CreateOrderAsync(ZPCreateOrderRequest zaloPayOrder)
    {
        var url = ZalopayBaseUrl + CreateOrderUrl;
        var result = await PostMethodAsync<ZPCreateOrderResponse>(url, zaloPayOrder);
        return result;
    }

    public async Task<ZPQueryOrderResponse?> QueryOrderAsync(string appTransId)
    {
        var data = $"{_zaloPayConfig.AppId}|{appTransId}|{_zaloPayConfig.Key1}";
        var mac = CryptoHelper.HMacCompute(_zaloPayConfig.Key1, data);
        var orderQuery = new ZPQueryOrderRequest { AppId = _zaloPayConfig.AppId, AppTransId = appTransId, Mac = mac };
        return await PostMethodAsync<ZPQueryOrderResponse>(ZalopayBaseUrl + QueryOrderUrl, orderQuery);
    }

    public bool ValidateCallback(ZPCallbackOrderResponse callbackOrder)
    {
        var mac = CryptoHelper.HMacCompute(_zaloPayConfig.Key2, callbackOrder.Data);
        return mac.Equals(callbackOrder.Mac);
    }

    private async Task<T?> PostMethodAsync<T>(string url, IBaseFormRequest form)
    {
        var content = new FormUrlEncodedContent(form.ToDictionary());
        var response = await _httpClient.PostAsync(url, content);
        var responseString = await response.Content.ReadAsStringAsync();

        // convert json to object
        var contractResolver = new DefaultContractResolver
        {
            NamingStrategy = new SnakeCaseNamingStrategy()
        };
        var result = JsonConvert.DeserializeObject<T>(responseString, new JsonSerializerSettings
        {
            ContractResolver = contractResolver,
            Formatting = Formatting.Indented
        });
        return result;
    }

    public async Task<ZPQueryUserResponse?> QueryZalopayUserAsync(UserQueryModel model)
    {
        var appTime = CurrentTime.GetTimeStamp();
        var data = $"{_zaloPayConfig.Dibursement.AppId}|{model.Phone}|{appTime}";
        var mac = CryptoHelper.HMacCompute(_zaloPayConfig.Dibursement.Key1, data);
        var requestModel = new ZPQueryUserRequest
        {
            AppId = _zaloPayConfig.Dibursement.AppId,
            Phone = model.Phone,
            Time = appTime,
            Mac = mac
        };
        var url = ZalopayBaseUrl + QueryUserUrl;
        var result = await PostMethodAsync<ZPQueryUserResponse>(url, requestModel);
        return result;
    }

    public async Task<ZPTopupResponse?> ZalopayTopupAsync(TopupModel model, Guid transactionId)
    {
        var currentUsername = _claimService.GetCurrentUserName;

        var appTime = CurrentTime.GetTimeStamp();
        var appId = _zaloPayConfig.Dibursement.AppId;
        var paymentId = _zaloPayConfig.Dibursement.PaymentId;
        var partnerOrderId = transactionId.ToString();
        var description = $"Rút xu nền tảng Artlink tài khoản {currentUsername} số xu {model.Amount}";
        var partnerEmbedData = "{\"store_id\":\"a1\",\"store_name\":\"Artworkia\"}";
        var extraInfo = "{}";


        var data = $"{appId}|{paymentId}|{partnerOrderId}|{model.MUId}|{model.Amount}" +
                    $"|{description}|{partnerEmbedData}|{extraInfo}|{appTime}";
        var hmacoutput = CryptoHelper.HMacCompute(_zaloPayConfig.Dibursement.Key1, data);
        var signed = CryptoHelper.RSASign(_zaloPayConfig.Dibursement.PrivateKey, hmacoutput);

        var requestModel = new ZPTopupRequest
        {
            AppId = appId,
            PaymentId = paymentId,
            Amount = model.Amount,
            Description = description,
            ExtraInfo = extraInfo,
            MUId = model.MUId,
            PartnerEmbedData = partnerEmbedData,
            PartnerOrderId = partnerOrderId,
            ReferenceId = model.ReferenceId,
            Time = appTime,
            Sig = signed
        };
        var url = ZalopayBaseUrl + TopupUrl;
        var result = await PostMethodAsync<ZPTopupResponse>(url, requestModel);
        return result;
    }

    public async Task<ZPQueryMerchantBalanceResponse?> QueryMerchantBalanceAsync()
    {
        var appTime = CurrentTime.GetTimeStamp();
        var appId = _zaloPayConfig.Dibursement.AppId;
        var paymentId = _zaloPayConfig.Dibursement.PaymentId;
        var data = $"{appId}|{paymentId}|{appTime}";
        var mac = CryptoHelper.HMacCompute(_zaloPayConfig.Dibursement.Key1, data);

        var requestModel = new ZPQueryMerchantBalanceRequest
        {
            RequestId = Guid.NewGuid().ToString(),
            AppId = appId,
            PaymentId = paymentId,
            Time = appTime,
            Mac = mac
        };
        var url = ZalopayBaseUrl + QueryMerchantBalanceUrl;
        var result = await PostMethodAsync<ZPQueryMerchantBalanceResponse>(url, requestModel);
        return result;
    }
}
