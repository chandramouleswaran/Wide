using System.ComponentModel;

namespace Wide.Utils
{
    public class PropertyChangedExtendedEventArgs : PropertyChangedEventArgs
    {
        public virtual object OldValue { get; private set; }
        public virtual object NewValue { get; private set; }
        public virtual string Description { get; private set; }

        public PropertyChangedExtendedEventArgs(string propertyName, object oldValue, object newValue, string description) : base(propertyName)
        {
            OldValue = oldValue;
            NewValue = newValue;
            Description = description;
        }
    }
}
