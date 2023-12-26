namespace SF.Blog.UnitTests.Core;
internal class MockDomainEntity : IDomainEntity
{
	public string Id { get; set; }
	public string OwnerId { get; set; }

    public MockDomainEntity()
    {
        Id = Guid.NewGuid().ToString();
        OwnerId = Id;
    }
}
