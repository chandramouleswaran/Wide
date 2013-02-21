using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace Wide.Settings
{
    public interface ISettingsManager
    {
        AbstractSettings Get(string key);
        bool Add(AbstractSettings settings);
    }

    internal class WideSettingsManager : AbstractSettings, ISettingsManager
    {
        public WideSettingsManager() : base()
        {
        }

        //Don't need a view for the settings manager
        public override ContentControl View
        {
            get { return null; }
        }

        public override void Load()
        {
            throw new NotImplementedException("Don't need to load a WideSettingsManager");
        }

        public override object Clone()
        {
            //Need a clone
            return null;
        }

        public override void Reset()
        {
            foreach (AbstractSettings settings in Children)
            {
                settings.Reset();
            }
        }
    }
}
