namespace wsmcbl.src.View.Components.Dto;

public static class Utilities
{
    public static DateOnlyDto MapToDto(this DateOnly dateOnly) => new(dateOnly);
    
    public static DateOnlyDto? MapToDto(this DateOnly? dateOnly) => dateOnly?.MapToDto();
}