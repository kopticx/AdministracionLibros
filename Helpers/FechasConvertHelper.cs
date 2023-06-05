namespace AdministracionLibros.Helpers
{
    public class FechasConvertHelper
    {
        public static string FormatoFechaDetalles(string fecha)
        {
            var sub = fecha.Substring(0, 10);

            var split = sub.Split("/");

            var newDate = $"{split[2]}/{split[0]}/{split[1]}";

            return DateTime.Parse(newDate).ToString("dd-MMMM-yyyy");
        }

        public static string FormatoFechaActualizaciones(string fecha)
        {
            if(fecha is not null)
            {
                var sub = fecha.Substring(0, 10);

                var split = sub.Split("/");

                var newDate = $"{split[2]}/{split[0]}/{split[1]}";

                return DateTime.Parse(newDate).ToString("yyyy-MMMM-dd");
            }

            return string.Empty;
        }
    }
}
