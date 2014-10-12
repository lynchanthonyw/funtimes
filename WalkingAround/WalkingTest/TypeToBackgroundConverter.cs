using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WalkingTest
{
    public sealed class TypeToBackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var methodName = parameter as string;
            if (value == null || methodName == null)
                return value;
            var methodInfo = value.GetType().GetMethod(methodName, new Type[0]);
            if (methodInfo == null)
                return value;
            return methodInfo.Invoke(value, new object[0]);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("TypeToBackgroundConverter can only be used for one way conversion.");
        }
    }
}
