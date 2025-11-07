using Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configuration;

public class SoftwareUsedConfiguration : IEntityTypeConfiguration<SoftwareUsed>
{
    public void Configure(EntityTypeBuilder<SoftwareUsed> builder)
    {
        builder.ToTable(nameof(SoftwareUsed));
        builder.Property(x => x.Id).HasDefaultValueSql("newid()");
        builder.HasIndex(x => x.SoftwareName).IsUnique();

        // relationship
        builder.HasMany(x => x.SoftwareDetails).WithOne(a => a.SoftwareUsed).HasForeignKey(a => a.SoftwareUsedId);

        #region init data
        builder.HasData
        (
            new SoftwareUsed()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                SoftwareName = "Adobe Photoshop"
            },
            new SoftwareUsed()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                SoftwareName = "Adobe Illustrator"
            },
            new SoftwareUsed()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                SoftwareName = "Adobe After Effect"
            },
            new SoftwareUsed()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000004"),
                SoftwareName = "Adobe Premiere"
            },
            new SoftwareUsed()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                SoftwareName = "Adobe Lightroom"
            },
            new SoftwareUsed()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000006"),
                SoftwareName = "Adobe XD"
            },
            new SoftwareUsed()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000007"),
                SoftwareName = "Figma"
            },
            new SoftwareUsed()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                SoftwareName = "Sketch"
            },
            new SoftwareUsed()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000009"),
                SoftwareName = "CorelDRAW"
            },
            new SoftwareUsed()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000000a"),
                SoftwareName = "Inkscape"
            },
            new SoftwareUsed()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000000b"),
                SoftwareName = "Blender"
            },
            new SoftwareUsed()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000000c"),
                SoftwareName = "Cinema 4D"
            },
            new SoftwareUsed()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000000d"),
                SoftwareName = "Maya"
            },
            new SoftwareUsed()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000000e"),
                SoftwareName = "ZBrush"
            },
            new SoftwareUsed()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000000f"),
                SoftwareName = "Substance Painter"
            },
            new SoftwareUsed()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000010"),
                SoftwareName = "Substance Designer"
            },
            new SoftwareUsed()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000011"),
                SoftwareName = "Substance Alchemist"
            },
            new SoftwareUsed()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000012"),
                SoftwareName = "Marvelous Designer"
            },
            new SoftwareUsed()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000013"),
                SoftwareName = "KeyShot"
            },
            new SoftwareUsed()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000014"),
                SoftwareName = "Lumion"
            },
            new SoftwareUsed()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000015"),
                SoftwareName = "AutoCAD"
            },
            new SoftwareUsed()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000016"),
                SoftwareName = "Revit"
            },
            new SoftwareUsed()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000017"),
                SoftwareName = "3ds Max"
            },
            new SoftwareUsed()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000018"),
                SoftwareName = "Rhino"
            },
            new SoftwareUsed()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000019"),
                SoftwareName = "Grasshopper"
            },
            new SoftwareUsed()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000001a"),
                SoftwareName = "Vectorworks"
            },
            new SoftwareUsed()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000001b"),
                SoftwareName = "ArchiCAD"
            },
            new SoftwareUsed()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000001c"),
                SoftwareName = "SketchUp"
            },
            new SoftwareUsed()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000001d"),
                SoftwareName = "Photoshop Lightroom"
            },
            new SoftwareUsed()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000001e"),
                SoftwareName = "Photoshop Elements"
            },
            new SoftwareUsed()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000001f"),
                SoftwareName = "PaintShop Pro"
            },
            new SoftwareUsed()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000020"),
                SoftwareName = "GIMP"
            },
            new SoftwareUsed()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000021"),
                SoftwareName = "Affinity Photo"
            },
            new SoftwareUsed()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000022"),
                SoftwareName = "Affinity Designer"
            },
            new SoftwareUsed()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000023"),
                SoftwareName = "Affinity Publisher"
            },
            new SoftwareUsed()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000024"),
                SoftwareName = "Clip Studio Paint"
            },
            new SoftwareUsed()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000025"),
                SoftwareName = "Krita"
            },
            new SoftwareUsed()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000026"),
                SoftwareName = "MediBang Paint"
            },
            new SoftwareUsed()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000027"),
                SoftwareName = "Procreate"
            },
            new SoftwareUsed()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000028"),
                SoftwareName = "ArtRage"
            },
            new SoftwareUsed()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000029"),
                SoftwareName = "Rebelle"
            },
            new SoftwareUsed()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000002a"),
                SoftwareName = "TwistedBrush Pro Studio"
            },
            new SoftwareUsed()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000002b"),
                SoftwareName = "Canva"
            },
            new SoftwareUsed()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000002c"),
                SoftwareName = "Paint Tool SAI"
            },
            new SoftwareUsed()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000002d"),
                SoftwareName = "Artweaver"
            },
            new SoftwareUsed()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000002e"),
                SoftwareName = "MyPaint"
            },
            new SoftwareUsed()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000002f"),
                SoftwareName = "FireAlpaca"
            },
            new SoftwareUsed()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000030"),
                SoftwareName = "OpenCanvas"
            },
            new SoftwareUsed()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000031"),
                SoftwareName = "Paint.NET"
            },
            new SoftwareUsed()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000032"),
                SoftwareName = "Pixia"
            },
            new SoftwareUsed()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000033"),
                SoftwareName = "SmoothDraw"
            },
            new SoftwareUsed()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000034"),
                SoftwareName = "Tayasui Sketches"
            },
            new SoftwareUsed()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000035"),
                SoftwareName = "Unity"
            },
            new SoftwareUsed()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000036"),
                SoftwareName = "Unreal Engine"
            },
            new SoftwareUsed()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000037"),
                SoftwareName = "GameMaker Studio"
            },
            new SoftwareUsed()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000038"),
                SoftwareName = "Godot Engine"
            },
            new SoftwareUsed()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000039"),
                SoftwareName = "Stable Diffusion"
            },
            new SoftwareUsed()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000003a"),
                SoftwareName = "Midjourney"
            }
        );
        #endregion
    }
}
