namespace BuberDinner.Contracts.Menus;

public record MenuResponse
(
    Guid Id,
    string Name,
    string Description,
    AverageRating AverageRating,
    List<MenuSectionResponse> Sections,
    string HostId,
    List<string> DinnerIds,
    List<string> MenuReviewIds,
    DateTime CreatedDateTime,
    DateTime UpdatedDateTime
);
public record AverageRating
(
    float Value,
    int NumRatings
);
public record MenuSectionResponse
(
    Guid Id,
    string Name,
    string Description,
    List<MenuItemResponse> Items
);
public record MenuItemResponse
(
    Guid Id,
    string Name,
    string Description
);