namespace wsmcbl.src.View.Components.Dto;

public static class Utilities
{
    public static DateOnlyDto ToEntity(this DateOnly dateOnly) => new(dateOnly);
    
    public static DateOnlyDto? ToEntityOrNull(this DateOnly? dateOnly) => dateOnly?.ToEntity();
}