namespace RecordingTrackerApi.Results;
public class ValidateRelationshipsResult
{
    public bool IsValid { get; }
    public string ErrorMessage { get; }

    private ValidateRelationshipsResult(bool isValid, string errorMessage = "")
    {
        IsValid = isValid;
        ErrorMessage = errorMessage;
    }

    public static ValidateRelationshipsResult Valid()
    {
        return new ValidateRelationshipsResult(true);
    }

    public static ValidateRelationshipsResult Invalid(string errorMessage)
    {
        return new ValidateRelationshipsResult(false, errorMessage);
    }
}
