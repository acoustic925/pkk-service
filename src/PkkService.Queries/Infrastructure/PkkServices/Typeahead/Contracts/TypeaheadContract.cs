namespace PkkService.Queries.Infrastructure.PkkServices.Typeahead.Contracts
{
    /// <summary>
    /// Контракт метода /typeahead API ПКК
    /// </summary>
    public class TypeaheadContract
    { 
        public IEnumerable<TypeaheadObject> Results { get; set; }
    }

    /// <summary>
    /// Контракт сматченого объекта ПКК 
    /// </summary>
    public class TypeaheadObject
    {

        public string Title { get; set; }

        public string Value { get; set; }
    }
}
