using System;
using System.ComponentModel.DataAnnotations;

namespace Affixx.Core.Database.Generated
{
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable ConditionIsAlwaysTrueOrFalse
		public abstract class TableEntity
		{
			internal abstract void EntitySetId(long id);

			internal abstract string EntityInsertSql { get; }
			internal abstract string EntityUpdateSql { get; }

			public abstract long Id { get; set; }
		}

		public partial class Comment : TableEntity
		{
			public long DocumentId { get; set; }
			public string Text { get; set; }
			public long CreatedBy { get; set; }
			public DateTime CreatedAt { get; set; }
			public DateTime? DeletedAt { get; set; }
			public override long Id { get; set; }

			internal override void EntitySetId(long id) {  Id = id; }
			internal override string EntityInsertSql { get { return "INSERT INTO Comments([CreatedAt], [CreatedBy], [DeletedAt], [DocumentId], [Text]) VALUES (@CreatedAt, @CreatedBy, @DeletedAt, @DocumentId, @Text)";} }
			internal override string EntityUpdateSql {  get { return "UPDATE Comments SET [CreatedAt] = @CreatedAt, [CreatedBy] = @CreatedBy, [DeletedAt] = @DeletedAt, [DocumentId] = @DocumentId, [Text] = @Text  WHERE [Id] = @Id"; } }

			public override bool Equals(object other)
			{
				if (ReferenceEquals(this, other)) {
					return true;
				}

				var entity = other as Comment;
				if (entity == null) {
					return false;
				}

				if (Id != entity.Id) {
					return false;
				}
				if (DocumentId != entity.DocumentId) {
					return false;
				}
				if (Text != entity.Text) {
					return false;
				}
				if (CreatedBy != entity.CreatedBy) {
					return false;
				}
				if (CreatedAt != entity.CreatedAt) {
					return false;
				}
				if (DeletedAt != entity.DeletedAt) {
					return false;
				}
				return true;
			}

			public override int GetHashCode()
			{
				unchecked {
					const int randomPrime = 397;
					int hashCode = Id.GetHashCode();
					hashCode = (hashCode*randomPrime) ^ DocumentId.GetHashCode();
					hashCode = (hashCode*randomPrime) ^ (Text != null ? Text.GetHashCode() : 0);
					hashCode = (hashCode*randomPrime) ^ CreatedBy.GetHashCode();
					hashCode = (hashCode*randomPrime) ^ CreatedAt.GetHashCode();
					hashCode = (hashCode*randomPrime) ^ (DeletedAt != null ? DeletedAt.GetHashCode() : 0);
					return hashCode;
				}
			}
		}
		
		public partial class DocumentCategory : TableEntity
		{
			public string Name { get; set; }
			public override long Id { get; set; }

			internal override void EntitySetId(long id) {  Id = id; }
			internal override string EntityInsertSql { get { return "INSERT INTO DocumentCategories([Name]) VALUES (@Name)";} }
			internal override string EntityUpdateSql {  get { return "UPDATE DocumentCategories SET [Name] = @Name  WHERE [Id] = @Id"; } }

			public override bool Equals(object other)
			{
				if (ReferenceEquals(this, other)) {
					return true;
				}

				var entity = other as DocumentCategory;
				if (entity == null) {
					return false;
				}

				if (Id != entity.Id) {
					return false;
				}
				if (Name != entity.Name) {
					return false;
				}
				return true;
			}

			public override int GetHashCode()
			{
				unchecked {
					const int randomPrime = 397;
					int hashCode = Id.GetHashCode();
					hashCode = (hashCode*randomPrime) ^ (Name != null ? Name.GetHashCode() : 0);
					return hashCode;
				}
			}
		}
		
		public partial class DocumentClaim : TableEntity
		{
			public long UserId { get; set; }
			public int AvailableFree { get; set; }
			public int RemainingQuota { get; set; }
			public string Reason { get; set; }
			public override long Id { get; set; }

			internal override void EntitySetId(long id) {  Id = id; }
			internal override string EntityInsertSql { get { return "INSERT INTO DocumentClaims([AvailableFree], [Reason], [RemainingQuota], [UserId]) VALUES (@AvailableFree, @Reason, @RemainingQuota, @UserId)";} }
			internal override string EntityUpdateSql {  get { return "UPDATE DocumentClaims SET [AvailableFree] = @AvailableFree, [Reason] = @Reason, [RemainingQuota] = @RemainingQuota, [UserId] = @UserId  WHERE [Id] = @Id"; } }

			public override bool Equals(object other)
			{
				if (ReferenceEquals(this, other)) {
					return true;
				}

				var entity = other as DocumentClaim;
				if (entity == null) {
					return false;
				}

				if (Id != entity.Id) {
					return false;
				}
				if (UserId != entity.UserId) {
					return false;
				}
				if (AvailableFree != entity.AvailableFree) {
					return false;
				}
				if (RemainingQuota != entity.RemainingQuota) {
					return false;
				}
				if (Reason != entity.Reason) {
					return false;
				}
				return true;
			}

			public override int GetHashCode()
			{
				unchecked {
					const int randomPrime = 397;
					int hashCode = Id.GetHashCode();
					hashCode = (hashCode*randomPrime) ^ UserId.GetHashCode();
					hashCode = (hashCode*randomPrime) ^ AvailableFree.GetHashCode();
					hashCode = (hashCode*randomPrime) ^ RemainingQuota.GetHashCode();
					hashCode = (hashCode*randomPrime) ^ (Reason != null ? Reason.GetHashCode() : 0);
					return hashCode;
				}
			}
		}
		
		public partial class DocumentRating : TableEntity
		{
			public long DocumentId { get; set; }
			public int Rating { get; set; }
			public long CreatedBy { get; set; }
			public DateTime CreatedAt { get; set; }
			public override long Id { get; set; }

			internal override void EntitySetId(long id) {  Id = id; }
			internal override string EntityInsertSql { get { return "INSERT INTO DocumentRatings([CreatedAt], [CreatedBy], [DocumentId], [Rating]) VALUES (@CreatedAt, @CreatedBy, @DocumentId, @Rating)";} }
			internal override string EntityUpdateSql {  get { return "UPDATE DocumentRatings SET [CreatedAt] = @CreatedAt, [CreatedBy] = @CreatedBy, [DocumentId] = @DocumentId, [Rating] = @Rating  WHERE [Id] = @Id"; } }

			public override bool Equals(object other)
			{
				if (ReferenceEquals(this, other)) {
					return true;
				}

				var entity = other as DocumentRating;
				if (entity == null) {
					return false;
				}

				if (Id != entity.Id) {
					return false;
				}
				if (DocumentId != entity.DocumentId) {
					return false;
				}
				if (Rating != entity.Rating) {
					return false;
				}
				if (CreatedBy != entity.CreatedBy) {
					return false;
				}
				if (CreatedAt != entity.CreatedAt) {
					return false;
				}
				return true;
			}

			public override int GetHashCode()
			{
				unchecked {
					const int randomPrime = 397;
					int hashCode = Id.GetHashCode();
					hashCode = (hashCode*randomPrime) ^ DocumentId.GetHashCode();
					hashCode = (hashCode*randomPrime) ^ Rating.GetHashCode();
					hashCode = (hashCode*randomPrime) ^ CreatedBy.GetHashCode();
					hashCode = (hashCode*randomPrime) ^ CreatedAt.GetHashCode();
					return hashCode;
				}
			}
		}
		
		public partial class DocumentView : TableEntity
		{
			public long DocumentId { get; set; }
			public long UserId { get; set; }
			public DateTime ViewedAt { get; set; }
			public override long Id { get; set; }

			internal override void EntitySetId(long id) {  Id = id; }
			internal override string EntityInsertSql { get { return "INSERT INTO DocumentViews([DocumentId], [UserId], [ViewedAt]) VALUES (@DocumentId, @UserId, @ViewedAt)";} }
			internal override string EntityUpdateSql {  get { return "UPDATE DocumentViews SET [DocumentId] = @DocumentId, [UserId] = @UserId, [ViewedAt] = @ViewedAt  WHERE [Id] = @Id"; } }

			public override bool Equals(object other)
			{
				if (ReferenceEquals(this, other)) {
					return true;
				}

				var entity = other as DocumentView;
				if (entity == null) {
					return false;
				}

				if (Id != entity.Id) {
					return false;
				}
				if (DocumentId != entity.DocumentId) {
					return false;
				}
				if (UserId != entity.UserId) {
					return false;
				}
				if (ViewedAt != entity.ViewedAt) {
					return false;
				}
				return true;
			}

			public override int GetHashCode()
			{
				unchecked {
					const int randomPrime = 397;
					int hashCode = Id.GetHashCode();
					hashCode = (hashCode*randomPrime) ^ DocumentId.GetHashCode();
					hashCode = (hashCode*randomPrime) ^ UserId.GetHashCode();
					hashCode = (hashCode*randomPrime) ^ ViewedAt.GetHashCode();
					return hashCode;
				}
			}
		}
		
		public partial class Document : TableEntity
		{
			public string FullDocumentFileName { get; set; }
			public string FreeSnippetFileName { get; set; }
			public string OriginalFileName { get; set; }
			public string Title { get; set; }
			public string Description { get; set; }
			public string Degree { get; set; }
			public string CourseName { get; set; }
			public string CourseCode { get; set; }
			public string Year { get; set; }
			public long UniversityId { get; set; }
			public long UploadedBy { get; set; }
			public DateTime UploadedAt { get; set; }
			public DateTime? DeletedAt { get; set; }
			public bool IsFree { get; set; }
			public bool? IsApproved { get; set; }
			public string Hash { get; set; }
			public string MinHashSignature { get; set; }
			public long CategoryId { get; set; }
			public override long Id { get; set; }

			internal override void EntitySetId(long id) {  Id = id; }
			internal override string EntityInsertSql { get { return "INSERT INTO Documents([CategoryId], [CourseCode], [CourseName], [Degree], [DeletedAt], [Description], [FreeSnippetFileName], [FullDocumentFileName], [Hash], [IsApproved], [IsFree], [MinHashSignature], [OriginalFileName], [Title], [UniversityId], [UploadedAt], [UploadedBy], [Year]) VALUES (@CategoryId, @CourseCode, @CourseName, @Degree, @DeletedAt, @Description, @FreeSnippetFileName, @FullDocumentFileName, @Hash, @IsApproved, @IsFree, @MinHashSignature, @OriginalFileName, @Title, @UniversityId, @UploadedAt, @UploadedBy, @Year)";} }
			internal override string EntityUpdateSql {  get { return "UPDATE Documents SET [CategoryId] = @CategoryId, [CourseCode] = @CourseCode, [CourseName] = @CourseName, [Degree] = @Degree, [DeletedAt] = @DeletedAt, [Description] = @Description, [FreeSnippetFileName] = @FreeSnippetFileName, [FullDocumentFileName] = @FullDocumentFileName, [Hash] = @Hash, [IsApproved] = @IsApproved, [IsFree] = @IsFree, [MinHashSignature] = @MinHashSignature, [OriginalFileName] = @OriginalFileName, [Title] = @Title, [UniversityId] = @UniversityId, [UploadedAt] = @UploadedAt, [UploadedBy] = @UploadedBy, [Year] = @Year  WHERE [Id] = @Id"; } }

			public override bool Equals(object other)
			{
				if (ReferenceEquals(this, other)) {
					return true;
				}

				var entity = other as Document;
				if (entity == null) {
					return false;
				}

				if (Id != entity.Id) {
					return false;
				}
				if (FullDocumentFileName != entity.FullDocumentFileName) {
					return false;
				}
				if (FreeSnippetFileName != entity.FreeSnippetFileName) {
					return false;
				}
				if (OriginalFileName != entity.OriginalFileName) {
					return false;
				}
				if (Title != entity.Title) {
					return false;
				}
				if (Description != entity.Description) {
					return false;
				}
				if (Degree != entity.Degree) {
					return false;
				}
				if (CourseName != entity.CourseName) {
					return false;
				}
				if (CourseCode != entity.CourseCode) {
					return false;
				}
				if (Year != entity.Year) {
					return false;
				}
				if (UniversityId != entity.UniversityId) {
					return false;
				}
				if (UploadedBy != entity.UploadedBy) {
					return false;
				}
				if (UploadedAt != entity.UploadedAt) {
					return false;
				}
				if (DeletedAt != entity.DeletedAt) {
					return false;
				}
				if (IsFree != entity.IsFree) {
					return false;
				}
				if (IsApproved != entity.IsApproved) {
					return false;
				}
				if (Hash != entity.Hash) {
					return false;
				}
				if (MinHashSignature != entity.MinHashSignature) {
					return false;
				}
				if (CategoryId != entity.CategoryId) {
					return false;
				}
				return true;
			}

			public override int GetHashCode()
			{
				unchecked {
					const int randomPrime = 397;
					int hashCode = Id.GetHashCode();
					hashCode = (hashCode*randomPrime) ^ (FullDocumentFileName != null ? FullDocumentFileName.GetHashCode() : 0);
					hashCode = (hashCode*randomPrime) ^ (FreeSnippetFileName != null ? FreeSnippetFileName.GetHashCode() : 0);
					hashCode = (hashCode*randomPrime) ^ (OriginalFileName != null ? OriginalFileName.GetHashCode() : 0);
					hashCode = (hashCode*randomPrime) ^ (Title != null ? Title.GetHashCode() : 0);
					hashCode = (hashCode*randomPrime) ^ (Description != null ? Description.GetHashCode() : 0);
					hashCode = (hashCode*randomPrime) ^ (Degree != null ? Degree.GetHashCode() : 0);
					hashCode = (hashCode*randomPrime) ^ (CourseName != null ? CourseName.GetHashCode() : 0);
					hashCode = (hashCode*randomPrime) ^ (CourseCode != null ? CourseCode.GetHashCode() : 0);
					hashCode = (hashCode*randomPrime) ^ (Year != null ? Year.GetHashCode() : 0);
					hashCode = (hashCode*randomPrime) ^ UniversityId.GetHashCode();
					hashCode = (hashCode*randomPrime) ^ UploadedBy.GetHashCode();
					hashCode = (hashCode*randomPrime) ^ UploadedAt.GetHashCode();
					hashCode = (hashCode*randomPrime) ^ (DeletedAt != null ? DeletedAt.GetHashCode() : 0);
					hashCode = (hashCode*randomPrime) ^ IsFree.GetHashCode();
					hashCode = (hashCode*randomPrime) ^ (IsApproved != null ? IsApproved.GetHashCode() : 0);
					hashCode = (hashCode*randomPrime) ^ (Hash != null ? Hash.GetHashCode() : 0);
					hashCode = (hashCode*randomPrime) ^ (MinHashSignature != null ? MinHashSignature.GetHashCode() : 0);
					hashCode = (hashCode*randomPrime) ^ CategoryId.GetHashCode();
					return hashCode;
				}
			}
		}
		
		public partial class Follower : TableEntity
		{
			public long UserId { get; set; }
			public long FollowerId { get; set; }
			public DateTime FollowedAt { get; set; }
			public override long Id { get; set; }

			internal override void EntitySetId(long id) {  Id = id; }
			internal override string EntityInsertSql { get { return "INSERT INTO Followers([FollowedAt], [FollowerId], [UserId]) VALUES (@FollowedAt, @FollowerId, @UserId)";} }
			internal override string EntityUpdateSql {  get { return "UPDATE Followers SET [FollowedAt] = @FollowedAt, [FollowerId] = @FollowerId, [UserId] = @UserId  WHERE [Id] = @Id"; } }

			public override bool Equals(object other)
			{
				if (ReferenceEquals(this, other)) {
					return true;
				}

				var entity = other as Follower;
				if (entity == null) {
					return false;
				}

				if (Id != entity.Id) {
					return false;
				}
				if (UserId != entity.UserId) {
					return false;
				}
				if (FollowerId != entity.FollowerId) {
					return false;
				}
				if (FollowedAt != entity.FollowedAt) {
					return false;
				}
				return true;
			}

			public override int GetHashCode()
			{
				unchecked {
					const int randomPrime = 397;
					int hashCode = Id.GetHashCode();
					hashCode = (hashCode*randomPrime) ^ UserId.GetHashCode();
					hashCode = (hashCode*randomPrime) ^ FollowerId.GetHashCode();
					hashCode = (hashCode*randomPrime) ^ FollowedAt.GetHashCode();
					return hashCode;
				}
			}
		}
		
		public partial class Purchase : TableEntity
		{
			public long DocumentId { get; set; }
			public long BuyerId { get; set; }
			public DateTime PurchasedAt { get; set; }
			public string TransactionId { get; set; }
			public override long Id { get; set; }

			internal override void EntitySetId(long id) {  Id = id; }
			internal override string EntityInsertSql { get { return "INSERT INTO Purchases([BuyerId], [DocumentId], [PurchasedAt], [TransactionId]) VALUES (@BuyerId, @DocumentId, @PurchasedAt, @TransactionId)";} }
			internal override string EntityUpdateSql {  get { return "UPDATE Purchases SET [BuyerId] = @BuyerId, [DocumentId] = @DocumentId, [PurchasedAt] = @PurchasedAt, [TransactionId] = @TransactionId  WHERE [Id] = @Id"; } }

			public override bool Equals(object other)
			{
				if (ReferenceEquals(this, other)) {
					return true;
				}

				var entity = other as Purchase;
				if (entity == null) {
					return false;
				}

				if (Id != entity.Id) {
					return false;
				}
				if (DocumentId != entity.DocumentId) {
					return false;
				}
				if (BuyerId != entity.BuyerId) {
					return false;
				}
				if (PurchasedAt != entity.PurchasedAt) {
					return false;
				}
				if (TransactionId != entity.TransactionId) {
					return false;
				}
				return true;
			}

			public override int GetHashCode()
			{
				unchecked {
					const int randomPrime = 397;
					int hashCode = Id.GetHashCode();
					hashCode = (hashCode*randomPrime) ^ DocumentId.GetHashCode();
					hashCode = (hashCode*randomPrime) ^ BuyerId.GetHashCode();
					hashCode = (hashCode*randomPrime) ^ PurchasedAt.GetHashCode();
					hashCode = (hashCode*randomPrime) ^ (TransactionId != null ? TransactionId.GetHashCode() : 0);
					return hashCode;
				}
			}
		}
		
		public partial class Referral : TableEntity
		{
			public long UserId { get; set; }
			public string InvitedEmail { get; set; }
			public long? InvitedUserId { get; set; }
			public override long Id { get; set; }

			internal override void EntitySetId(long id) {  Id = id; }
			internal override string EntityInsertSql { get { return "INSERT INTO Referrals([InvitedEmail], [InvitedUserId], [UserId]) VALUES (@InvitedEmail, @InvitedUserId, @UserId)";} }
			internal override string EntityUpdateSql {  get { return "UPDATE Referrals SET [InvitedEmail] = @InvitedEmail, [InvitedUserId] = @InvitedUserId, [UserId] = @UserId  WHERE [Id] = @Id"; } }

			public override bool Equals(object other)
			{
				if (ReferenceEquals(this, other)) {
					return true;
				}

				var entity = other as Referral;
				if (entity == null) {
					return false;
				}

				if (Id != entity.Id) {
					return false;
				}
				if (UserId != entity.UserId) {
					return false;
				}
				if (InvitedEmail != entity.InvitedEmail) {
					return false;
				}
				if (InvitedUserId != entity.InvitedUserId) {
					return false;
				}
				return true;
			}

			public override int GetHashCode()
			{
				unchecked {
					const int randomPrime = 397;
					int hashCode = Id.GetHashCode();
					hashCode = (hashCode*randomPrime) ^ UserId.GetHashCode();
					hashCode = (hashCode*randomPrime) ^ (InvitedEmail != null ? InvitedEmail.GetHashCode() : 0);
					hashCode = (hashCode*randomPrime) ^ (InvitedUserId != null ? InvitedUserId.GetHashCode() : 0);
					return hashCode;
				}
			}
		}
		
		public partial class University : TableEntity
		{
			public string Name { get; set; }
			public override long Id { get; set; }

			internal override void EntitySetId(long id) {  Id = id; }
			internal override string EntityInsertSql { get { return "INSERT INTO Universities([Name]) VALUES (@Name)";} }
			internal override string EntityUpdateSql {  get { return "UPDATE Universities SET [Name] = @Name  WHERE [Id] = @Id"; } }

			public override bool Equals(object other)
			{
				if (ReferenceEquals(this, other)) {
					return true;
				}

				var entity = other as University;
				if (entity == null) {
					return false;
				}

				if (Id != entity.Id) {
					return false;
				}
				if (Name != entity.Name) {
					return false;
				}
				return true;
			}

			public override int GetHashCode()
			{
				unchecked {
					const int randomPrime = 397;
					int hashCode = Id.GetHashCode();
					hashCode = (hashCode*randomPrime) ^ (Name != null ? Name.GetHashCode() : 0);
					return hashCode;
				}
			}
		}
		
		public partial class UniversityCourse : TableEntity
		{
			public long UniversityId { get; set; }
			public string Group { get; set; }
			public string Name { get; set; }
			public string Code { get; set; }
			public long CreatedBy { get; set; }
			public DateTime CreatedAt { get; set; }
			public DateTime? DeletedAt { get; set; }
			public override long Id { get; set; }

			internal override void EntitySetId(long id) {  Id = id; }
			internal override string EntityInsertSql { get { return "INSERT INTO UniversityCourses([Code], [CreatedAt], [CreatedBy], [DeletedAt], [Group], [Name], [UniversityId]) VALUES (@Code, @CreatedAt, @CreatedBy, @DeletedAt, @Group, @Name, @UniversityId)";} }
			internal override string EntityUpdateSql {  get { return "UPDATE UniversityCourses SET [Code] = @Code, [CreatedAt] = @CreatedAt, [CreatedBy] = @CreatedBy, [DeletedAt] = @DeletedAt, [Group] = @Group, [Name] = @Name, [UniversityId] = @UniversityId  WHERE [Id] = @Id"; } }

			public override bool Equals(object other)
			{
				if (ReferenceEquals(this, other)) {
					return true;
				}

				var entity = other as UniversityCourse;
				if (entity == null) {
					return false;
				}

				if (Id != entity.Id) {
					return false;
				}
				if (UniversityId != entity.UniversityId) {
					return false;
				}
				if (Group != entity.Group) {
					return false;
				}
				if (Name != entity.Name) {
					return false;
				}
				if (Code != entity.Code) {
					return false;
				}
				if (CreatedBy != entity.CreatedBy) {
					return false;
				}
				if (CreatedAt != entity.CreatedAt) {
					return false;
				}
				if (DeletedAt != entity.DeletedAt) {
					return false;
				}
				return true;
			}

			public override int GetHashCode()
			{
				unchecked {
					const int randomPrime = 397;
					int hashCode = Id.GetHashCode();
					hashCode = (hashCode*randomPrime) ^ UniversityId.GetHashCode();
					hashCode = (hashCode*randomPrime) ^ (Group != null ? Group.GetHashCode() : 0);
					hashCode = (hashCode*randomPrime) ^ (Name != null ? Name.GetHashCode() : 0);
					hashCode = (hashCode*randomPrime) ^ (Code != null ? Code.GetHashCode() : 0);
					hashCode = (hashCode*randomPrime) ^ CreatedBy.GetHashCode();
					hashCode = (hashCode*randomPrime) ^ CreatedAt.GetHashCode();
					hashCode = (hashCode*randomPrime) ^ (DeletedAt != null ? DeletedAt.GetHashCode() : 0);
					return hashCode;
				}
			}
		}
		
		public partial class UserLogin : TableEntity
		{
			public string LoginProvider { get; set; }
			public string ProviderKey { get; set; }
			public long UserId { get; set; }
			public override long Id { get; set; }

			internal override void EntitySetId(long id) {  Id = id; }
			internal override string EntityInsertSql { get { return "INSERT INTO UserLogins([LoginProvider], [ProviderKey], [UserId]) VALUES (@LoginProvider, @ProviderKey, @UserId)";} }
			internal override string EntityUpdateSql {  get { return "UPDATE UserLogins SET [LoginProvider] = @LoginProvider, [ProviderKey] = @ProviderKey, [UserId] = @UserId  WHERE [Id] = @Id"; } }

			public override bool Equals(object other)
			{
				if (ReferenceEquals(this, other)) {
					return true;
				}

				var entity = other as UserLogin;
				if (entity == null) {
					return false;
				}

				if (Id != entity.Id) {
					return false;
				}
				if (LoginProvider != entity.LoginProvider) {
					return false;
				}
				if (ProviderKey != entity.ProviderKey) {
					return false;
				}
				if (UserId != entity.UserId) {
					return false;
				}
				return true;
			}

			public override int GetHashCode()
			{
				unchecked {
					const int randomPrime = 397;
					int hashCode = Id.GetHashCode();
					hashCode = (hashCode*randomPrime) ^ (LoginProvider != null ? LoginProvider.GetHashCode() : 0);
					hashCode = (hashCode*randomPrime) ^ (ProviderKey != null ? ProviderKey.GetHashCode() : 0);
					hashCode = (hashCode*randomPrime) ^ UserId.GetHashCode();
					return hashCode;
				}
			}
		}
		
		public partial class User : TableEntity
		{
			public string Name { get; set; }
			public string Email { get; set; }
			public string Password { get; set; }
			public DateTime CreatedAt { get; set; }
			public DateTime? EmailConfirmedAt { get; set; }
			public DateTime? DeletedAt { get; set; }
			public string InviteCode { get; set; }
			public long? UniversityId { get; set; }
			public string Role { get; set; }
			public string AcademicField { get; set; }
			public string AcademicResume { get; set; }
			public override long Id { get; set; }

			internal override void EntitySetId(long id) {  Id = id; }
			internal override string EntityInsertSql { get { return "INSERT INTO Users([AcademicField], [AcademicResume], [CreatedAt], [DeletedAt], [Email], [EmailConfirmedAt], [InviteCode], [Name], [Password], [Role], [UniversityId]) VALUES (@AcademicField, @AcademicResume, @CreatedAt, @DeletedAt, @Email, @EmailConfirmedAt, @InviteCode, @Name, @Password, @Role, @UniversityId)";} }
			internal override string EntityUpdateSql {  get { return "UPDATE Users SET [AcademicField] = @AcademicField, [AcademicResume] = @AcademicResume, [CreatedAt] = @CreatedAt, [DeletedAt] = @DeletedAt, [Email] = @Email, [EmailConfirmedAt] = @EmailConfirmedAt, [InviteCode] = @InviteCode, [Name] = @Name, [Password] = @Password, [Role] = @Role, [UniversityId] = @UniversityId  WHERE [Id] = @Id"; } }

			public override bool Equals(object other)
			{
				if (ReferenceEquals(this, other)) {
					return true;
				}

				var entity = other as User;
				if (entity == null) {
					return false;
				}

				if (Id != entity.Id) {
					return false;
				}
				if (Name != entity.Name) {
					return false;
				}
				if (Email != entity.Email) {
					return false;
				}
				if (Password != entity.Password) {
					return false;
				}
				if (CreatedAt != entity.CreatedAt) {
					return false;
				}
				if (EmailConfirmedAt != entity.EmailConfirmedAt) {
					return false;
				}
				if (DeletedAt != entity.DeletedAt) {
					return false;
				}
				if (InviteCode != entity.InviteCode) {
					return false;
				}
				if (UniversityId != entity.UniversityId) {
					return false;
				}
				if (Role != entity.Role) {
					return false;
				}
				if (AcademicField != entity.AcademicField) {
					return false;
				}
				if (AcademicResume != entity.AcademicResume) {
					return false;
				}
				return true;
			}

			public override int GetHashCode()
			{
				unchecked {
					const int randomPrime = 397;
					int hashCode = Id.GetHashCode();
					hashCode = (hashCode*randomPrime) ^ (Name != null ? Name.GetHashCode() : 0);
					hashCode = (hashCode*randomPrime) ^ (Email != null ? Email.GetHashCode() : 0);
					hashCode = (hashCode*randomPrime) ^ (Password != null ? Password.GetHashCode() : 0);
					hashCode = (hashCode*randomPrime) ^ CreatedAt.GetHashCode();
					hashCode = (hashCode*randomPrime) ^ (EmailConfirmedAt != null ? EmailConfirmedAt.GetHashCode() : 0);
					hashCode = (hashCode*randomPrime) ^ (DeletedAt != null ? DeletedAt.GetHashCode() : 0);
					hashCode = (hashCode*randomPrime) ^ (InviteCode != null ? InviteCode.GetHashCode() : 0);
					hashCode = (hashCode*randomPrime) ^ (UniversityId != null ? UniversityId.GetHashCode() : 0);
					hashCode = (hashCode*randomPrime) ^ (Role != null ? Role.GetHashCode() : 0);
					hashCode = (hashCode*randomPrime) ^ (AcademicField != null ? AcademicField.GetHashCode() : 0);
					hashCode = (hashCode*randomPrime) ^ (AcademicResume != null ? AcademicResume.GetHashCode() : 0);
					return hashCode;
				}
			}
		}
		
// ReSharper restore ConditionIsAlwaysTrueOrFalse
// ReSharper restore PartialTypeWithSinglePart
// ReSharper restore InconsistentNaming
}

