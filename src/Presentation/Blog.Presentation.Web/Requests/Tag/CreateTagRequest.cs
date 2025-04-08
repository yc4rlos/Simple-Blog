using System;

namespace Blog.Presentation.Web.Requests.Tag;

public record CreateOrUpdateTagRequest(string Name, IFormFile Image);
