namespace EcommerceProject.API.DTOs
{
    /// <summary>
    /// Use it at Create Endpoints
    /// or as representing model
    /// </summary>
    public record ProductWithCategoryDto : ProductDto
    {
        public ProductWithCategoryDto()
        {
            Category = new CategoryDto();
        }
        public CategoryDto Category { get; set; }
    }
    /// <summary>
    /// Use it in Update Endpoints
    /// Notice:if you use CategoryDto instead of CategoryUpDto below and using direct EF Update method, it would add new child entity in not existance cases
    /// </summary>
    public record ProductWithCategoryUpDto : ProductUpDto
    {
        public CategoryUpDto Category { get; set; } 
    }
}