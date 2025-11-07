using Application.Models;
using Application.Services.Abstractions;
using AutoMapper;
using Domain.Entitites;
using Domain.Enums;
using Domain.Repositories.Abstractions;

namespace Application.Services;

public class MilestoneService : IMilestoneService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public MilestoneService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task AddMilestoneToProposalAsync(Guid proposalId, string? details = "", ProposalStateEnum? state = null)
    {
        var proposal = await _unitOfWork.ProposalRepository.GetByIdAsync(proposalId);
        if (proposal == null)
        {
            throw new KeyNotFoundException("Không tìm thấy thỏa thuận.");
        }
        var milestone = new Milestone { ProposalId = proposalId };
        if (state != null) // udpate state case
        {
            switch (state)
            {
                case ProposalStateEnum.Accepted:
                    details = $"Thỏa thuận đã được chấp nhận";
                    break;
                case ProposalStateEnum.Declined:
                    details = "Thỏa thuận đã bị từ chối";
                    break;
                case ProposalStateEnum.Cancelled:
                    details = "Thỏa thuận đã bị hủy";
                    break;
            }
        }
        milestone.MilestoneName = details!;
        await _unitOfWork.MilestoneRepository.AddAsync(milestone);
    }

    public async Task<List<MilestoneVM>> GetMilestonesAsync(Guid proposalId)
    {
        var proposalExist = await _unitOfWork.ProposalRepository.IsExistedAsync(proposalId);
        if (!proposalExist)
        {
            throw new KeyNotFoundException("Không tìm thấy thỏa thuận.");
        }
        var milestones = await _unitOfWork.MilestoneRepository.GetAllMilstoneOfProposalAsync(proposalId);
        return _mapper.Map<List<MilestoneVM>>(milestones);
    }
}
