namespace WizzardControl
{
    internal class CheckBoxElement : ICRViewElements
    {
        private string _text;
        public string Text
        {
            get => _text;
            set { _text = value; }
        }

        private string _value;
        public string Value 
        {
            get => _value;
            set { _value = value; }
        }

        private bool _isSelected;
        public bool IsSelected 
        {
            get => _isSelected;
            set { _isSelected = value; }
        }
    }
}