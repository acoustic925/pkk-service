namespace PkkService.Queries.Models
{
    /// <summary>
    /// Модель ответа сервиса с информацией об адресах объектов ПКК
    /// </summary>
    public class TypeaheadResponseModel
    {
        /// <summary>
        /// Полное число записей
        /// </summary>
        public int? Total { get; set; }

        /// <summary>
        /// Список адрессов объектов ПКК
        /// </summary>
        public IEnumerable<TypeaheadObjectModel> Results { get; set; }
    }

    /// <summary>
    /// Модель адресов объектов ПКК
    /// </summary>
    public class TypeaheadObjectModel
    {
        public string Title { get; set; }

        public string Value { get; set; }
    }
}
