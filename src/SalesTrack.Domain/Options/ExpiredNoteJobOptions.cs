using SalesTrack.Abstractions.Domain;

namespace SalesTrack.Domain.Options;

public class ExpiredNoteJobOptions : BaseJobOptions
{
    public const string SectionName = $"Jobs:{nameof(ExpiredNoteJobOptions)}";
}
