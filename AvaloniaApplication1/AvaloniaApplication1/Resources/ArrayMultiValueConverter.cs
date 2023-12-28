using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Avalonia.Data.Converters;

namespace AvaloniaApplication1;

public class ArrayMultiValueConverter : IMultiValueConverter {
    public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture) {
        return values.ToArray();
    }
}