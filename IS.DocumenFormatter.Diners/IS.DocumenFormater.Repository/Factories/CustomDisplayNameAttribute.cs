namespace System.ComponentModel
{
    public class CustomDisplayNameAttribute : DisplayNameAttribute
    {
        private bool _isRequired;
        public bool IsRequired { get => _isRequired; set => _isRequired = value; }

        public CustomDisplayNameAttribute() : base()
        {
        }
        public CustomDisplayNameAttribute(string displayName) : base(displayName)
        {
        }
        public override string DisplayName { get => DisplayNameValue; }// + (IsRequired ? " * " : ""); }
    }
}
