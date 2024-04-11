using System.Security.Cryptography;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Windows.Markup;

namespace GravatarHashCalculator.ValueConverters;

internal class EmailToGravatarHashConverter : MarkupExtension, IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        string? s = value as string;
        if (s == null)
        {
            s = string.Empty;
        }
        return HashEmailForGravatar(s);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }


    /// <summary>
    /// Hashes an email address for gravatar.
    /// </summary>
    /// <param name="email">The email address.</param>
    /// <returns>The hash corresponding to the input email address.</returns>
    private static string HashEmailForGravatar(string email)
    {
        // Create a new instance of the hash algorithm.  
        HashAlgorithm hasher = SHA256.Create();

        // Convert the input string to a byte array and compute the hash.  
        byte[] data = hasher.ComputeHash(Encoding.Default.GetBytes(email));

        // Create a new Stringbuilder to collect the bytes  
        // and create a string.  
        StringBuilder sBuilder = new StringBuilder();

        // Loop through each byte of the hashed data  
        // and format each one as a hexadecimal string.  
        for (int i = 0; i < data.Length; i++)
        {
            sBuilder.Append(data[i].ToString("x2"));
        }

        return sBuilder.ToString();
    }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return this;
    }
}


