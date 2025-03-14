namespace PatikaSurvivorAPI.Entities
{
    public class CategoryEntity : BaseEntity
    {
        public string Name { get; set; }

        public List<CompetitorEntity> Competitors { get; set; }
    }
}
