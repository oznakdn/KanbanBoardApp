namespace KanbanBoard.WebMvc.Extensions;

public static class StatusNameExtension
{
    public static string NameReplaceAndUpperCase(this string statusName)
    {
        statusName = statusName.Replace('ı', 'i');
        statusName = statusName.Replace('i', 'I');
        statusName = statusName.Replace('ş', 's');
        statusName = statusName.Replace('ü', 'u');
        statusName = statusName.Replace('ö', 'o');
        statusName = statusName.Replace('ç', 'c');
        statusName = statusName.Replace('ğ', 'g');
        return statusName.ToUpper();
    }
}
