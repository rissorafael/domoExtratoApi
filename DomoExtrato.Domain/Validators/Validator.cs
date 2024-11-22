namespace DomoExtrato.Domain.Validators
{
    public abstract class Validator
    {
        public int StatusCode { get; set; }
        public string Description { get; set; }
        public bool IsValid { get; protected set; } = true;


        public void AddErrorValidation(int errorStatus, string description)
        {
            this.StatusCode = errorStatus;
            this.Description = description;
            this.IsValid = false;
        }

        public void AddWarningValidation(int warningStatus, string description)
        {

            this.StatusCode = warningStatus;
            this.Description = description;
            this.IsValid = false;
        }
    }
}
