namespace p48_lessons_asp.Models.CategoryRequestModels;
public class CreateCategoryModel
{
    public Guid? ParentCategoryId{get;set;}
    public string Title {get;set;}
    public string? Description{get;set;}

}