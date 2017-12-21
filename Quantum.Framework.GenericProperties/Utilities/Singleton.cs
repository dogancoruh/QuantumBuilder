using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quantum.Framework.GenericProperties.Utilities
{
    public class Singleton<T>
    {
        #region Singleton members

        private static T instance;

        public static T Instance
        {
            get
            {
                if (instance == null)
                    instance = Activator.CreateInstance<T>();

                return instance;
            }
        }

        #endregion
    }
}
