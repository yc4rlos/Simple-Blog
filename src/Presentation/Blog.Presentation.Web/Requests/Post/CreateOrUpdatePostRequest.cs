namespace Blog.Presentation.Web.Requests.Post;

public record CreateOrUpdatePostRequest(
    string Title,
    string Content,
    string Summary,
    DateTime PostDate,
    List<int> Tags,
    IFormFile? Image
);
