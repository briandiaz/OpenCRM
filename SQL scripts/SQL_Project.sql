--Internal Database--
----------------------------------------------------------------

create table [dbo].[Profile](
	ProfileId int IDENTITY(1, 1),
	Name nvarchar(50),
	AbbrevationName nvarchar(50),
	CreateDate datetime,
	UpdateDate datetime,
	Constraint [Pk Profile] Primary Key(ProfileId)
);

create table [dbo].[Objects](
	ObjectId int IDENTITY(1, 1),
	Name nvarchar(50),
	Constraint [PK Objects] Primary Key(ObjectId)
);

create table [dbo].[Object_Fields](
	ObjectFieldsId int IDENTITY(1, 1),
	ObjectId int,
	Name nvarchar(50),
	Constraint [PK Object_Fields] Primary Key(ObjectFieldsId),
	Constraint [FK Object_Fields Objects] Foreign Key(ObjectId) References [dbo].[Objects](ObjectId)
);

create table [dbo].[Profile_Object](
	ProfileObjectId int IDENTITY(1, 1),
	ProfileId int,
	ObjectId int,
	Constraint [PK Profile_Object] Primary Key(ProfileObjectId),
	Constraint [FK Profile_Object Profile] Foreign Key(ProfileId) References [dbo].[Profile](ProfileId),
	Constraint [FK Profile_Object Objects] Foreign Key(ObjectId) References [dbo].[Objects](ObjectId)
);

create table [dbo].[Profile_Object_Fields](
	ProfileObjectFieldsId int IDENTITY(1, 1),
	ProfileObjectId int,
	ObjectFieldsId int,
	[Read] bit,
	[Modify] bit,
	[Create] bit,
	Constraint [PK Profile_Object_Fields] Primary Key(ProfileObjectFieldsId),
	Constraint [FK Profile_Object_Fields Profile_Object] Foreign Key(ProfileObjectId) References [dbo].[Profile_Object](ProfileObjectId),
	Constraint [FK Profile_Object_Fields Object_Fields] Foreign Key(ObjectFieldsId) References [dbo].[Object_Fields](ObjectFieldsId)
);

create table [dbo].[User](
	UserId int IDENTITY(1, 1),
	Name nvarchar(50),
	LastName nvarchar(50),
	BirthDate date,
	Email nvarchar(50),
	UserName nvarchar(50),
	HashPassword nvarchar(255),
	ProfileId int,
	CreateDate datetime,
	UpdateDate datetime,
	Constraint [PK User] Primary Key(UserId),
	Constraint [PK User Profile] Foreign Key(ProfileId) References [dbo].[Profile](ProfileId)
);

--External Database--
-----------------------------------------------------------------------------
create table [dbo].[Industry](
	IndustryId int IDENTITY(1, 1),
	Name nvarchar(50),
	Constraint [PK Industry] Primary Key(IndustryId)
);

create table [dbo].[Rating](
	RatingId int IDENTITY(1, 1),
	Name nvarchar(50),
	Value int,
	Constraint [PK Rating] Primary Key(RatingId)
);

create table [dbo].[Country](
	CountryId int IDENTITY(1, 1),
	Name nvarchar(50),
	AbbrevationName char(2),
	AbbrevationName2 char(3),
	Constraint [PK Country] Primary Key(CountryId)
);

create table [dbo].[State](
	StateId int IDENTITY(1, 1),
	Name nvarchar(50),
	CountryId int,
	Constraint [PK State] Primary Key(StateId),
	Constraint [FK State Country] Foreign Key(countryId) References [dbo].[Country](CountryId)
);

create table [dbo].[Salutation](
	SalutationId int IDENTITY(1, 1),
	Name nvarchar(50),
	Constraint [PK Salutation] Primary Key(SalutationId)
);

create table [dbo].[Lead_Source](
	LeadSourceId int IDENTITY(1, 1),
	Name nvarchar(50),
	Constraint [PK Lead_Source] Primary Key(LeadSourceId)
);

create table [dbo].[Language](
	LanguageId int IDENTITY(1, 1),
	Name nvarchar(50),
	Constraint [PK Language] Primary Key(LanguageId)
);

create table [dbo].[Level_Language](
	LevelLanguageId int IDENTITY(1, 1),
	Name nvarchar(50),
	Constraint [PK Level_Language] Primary Key(LevelLanguageId)
);

create table [dbo].[Address_Type](
	AddressTypeId int IDENTITY(1, 1),
	Name nvarchar(50),
	Constraint [PK Address_Type] Primary Key(AddressTypeId)
);

create table [dbo].[Address](
	AddressId int IDENTITY(1, 1),
	Street nvarchar(50),
    City nvarchar(50),
	ZipCode nvarchar(5),
    StateId int,
    AddressTypeId int,
    Constraint [PK Address] Primary Key(AddressId),
	Constraint [FK Address State] Foreign Key(StateId) References [dbo].[State](StateId),
	Constraint [FK Address Address_Type] Foreign Key(AddressTypeId) References [dbo].[Address_Type](AddressTypeId)
);

-----------
--Account--
-----------

create table [dbo].[Account_Ownership](
	AccountOwnershipId int IDENTITY(1, 1),
	Name nvarchar(50),
	Constraint [PK Account_Ownership] Primary Key(AccountOwnershipid)
);

create table [dbo].[Account_Priority](
	AccountPriorityId int IDENTITY(1, 1),
	Name nvarchar(50),
	Constraint [PK Account_Priority] Primary Key(AccountPriorityId)
);

create table [dbo].[Account_SLA](
	AccountSLAId int IDENTITY(1, 1),
	Name nvarchar(50),
	Value int,
	Constraint [PK Account_SLA] Primary Key(AccountSLAId)
);

create table [dbo].[Account_Type](
	AccountTypeId int IDENTITY(1, 1),
	Name nvarchar(50),
	Constraint [PK Account_Type] Primary Key(AccountTypeId)
);

create table [dbo].[Account_Up_Sell_Opportunity](
	AccountUpSellOpportunityId int IDENTITY(1, 1),
	Name nvarchar(50),
	Constraint [PK Account_Up_Sell_Opportunity] Primary Key(AccountUpSellOpportunityId)
);

create table [dbo].[Account](
	AccountId int IDENTITY(1, 1),
	UserId int,
	Name nvarchar(50),
	AccountParent int,
    AccountBillingId int,
	AccountOwnerShipId int,
    AccountShippingId int,
    AccountTypeId int,
	AccountPriorityId int,
    AccountSLAId int,
    AccountUpSellOpportunityId int,
    AccountSite nvarchar(50),
    Active bit,
    AnualRevenue real,
    [Description] nvarchar(max),
    Employees int,
    FaxNumber nvarchar(20),
    IndustryId int,
    NumberOfLocation int,
    PhoneNumber nvarchar(20),
    RatingId int,
    TickerSymbol nvarchar(255),
    WebSite nvarchar(255),
    CreateBy int,
    CreateDate datetime,
    UpdateBy int,
    UpdateDate datetime,
    HiddenAccount bit,
    ViewDate datetime,
    Code nvarchar(25),
	SLAExpiration date,
    SlaSerialNumber nvarchar(255),
	Constraint [PK Account] Primary Key(Accountid),
	Constraint [FK Account Rating] Foreign Key(RatingId) References [dbo].[Rating](RatingId),
	Constraint [FK Account Industry] Foreign Key(IndustryId) References [dbo].[Industry](IndustryId),
	Constraint [FK Account Account_Parent] Foreign Key(AccountParent) References [dbo].[Account](AccountId),
	Constraint [FK Account User] Foreign Key(UserId) References [dbo].[User](UserId),
	Constraint [FK Account User_Creator] Foreign Key(CreateBy) References [dbo].[User](UserId),
	Constraint [FK Account User_Modificator] Foreign Key(UpdateBy) References [dbo].[User](UserId),
	Constraint [FK Account Address_Shipping] Foreign Key(accountShippingid) References [dbo].[Address](AddressId),
	Constraint [FK Account Address_Billing] Foreign Key(accountBillingid) References [dbo].[Address](AddressId),
	Constraint [FK Account Account_Ownership] Foreign Key(accountOwnerShipid) References [dbo].[Account_Ownership](accountOwnerShipid),
	Constraint [FK Account Account_Priority] Foreign Key(AccountPriorityId) References [dbo].[Account_Priority](AccountPriorityId),
	Constraint [FK Account Account_SLA] Foreign Key(accountSLAid) References [dbo].[Account_SLA](accountSLAid),
	Constraint [FK Account Account_Type] Foreign Key(accountTypeid) References [dbo].[Account_Type](accountTypeid),
	Constraint [FK Account Account_Up_Sell_Opportunity] Foreign Key(accountUpSellOpportunityid) References [dbo].[Account_Up_Sell_Opportunity](accountUpSellOpportunityid)
);

------------
--Contacts--
------------

create table [dbo].[Contact_Level_Languages](
	ContactLevelLanguagesId int IDENTITY(1, 1),
	LanguageId int,
	LevelLanguageId int,
	Constraint [PK Contact_Level_Languages] Primary Key(ContactLevelLanguagesId),
	Constraint [FK Contact_Level_Languages Language] Foreign Key(LanguageId) References [dbo].[Language](LanguageId),
	Constraint [FK Contact_Level_Languages Level_Language] Foreign Key(LevelLanguageId) References [dbo].[Level_Language](LevelLanguageId)
);

create table [dbo].[Contact](
	ContactId int IDENTITY(1, 1),
	UserId int,
	SalutationId int,
	FirstName nvarchar(50),
	LastName nvarchar(50),
	AccountId int,
	Title nvarchar(50),
	Department nvarchar(50),
	BirthDate date,
	ContactReportToId int,
	LeadSourceId int,
	PhoneNumber nvarchar(20),
	HomePhoneNumber nvarchar(20),
	MobileNumber nvarchar(20),
	OtherPhoneMobile nvarchar(20),
	FaxNumber nvarchar(20),
	Email nvarchar(50),
	Assitant nvarchar(50),
	AssitantPhoneNumber nvarchar(20),
	ContactMailingAddressId int,
	ContactOtherAddressId int,
	ContactLevelLanguagesId int,
	[Description] nvarchar(max),
	CreateBy int,
	CreateDate datetime,
	UpdateBy int,
	UpdateDate datetime,
	ViewDate datetime,
	HiddenContact bit,
	Constraint [PK Contact] Primary Key(ContactId),
	Constraint [FK Contact User] Foreign Key(UserId) References [dbo].[User](UserId),
	Constraint [FK Contact User_Creator] Foreign Key(CreateBy) References [dbo].[User](UserId),
	Constraint [FK Contact User_Modificator] Foreign Key(UpdateBy) References [dbo].[User](UserId),
	Constraint [FK Contact Salution] Foreign Key(SalutationId) References [dbo].[Salutation](SalutationId),
	Constraint [FK Contact Account] Foreign Key(AccountId) References [dbo].[Account](AccountId),
	Constraint [FK Contact Contact] Foreign Key(ContactReportToId) References [dbo].[Contact](ContactId),
	Constraint [FK Contact Lead_Source] Foreign Key(LeadSourceId) References [dbo].[Lead_Source](LeadSourceId),
	Constraint [FK Contact Address_Mailing] Foreign Key(ContactMailingAddressId) References [dbo].[Address](AddressId),
	Constraint [FK Contact Address_Other] Foreign Key(ContactOtherAddressId) References [dbo].[Address](AddressId),
	Constraint [FK Contact Contact_Level_Languages] Foreign Key(ContactLevelLanguagesId) References [dbo].[Contact_Level_Languages](ContactLevelLanguagesId)
);

------------
--Campaign--
------------

create table [dbo].[Campaign_Type](
	CampaignTypeId int IDENTITY(1, 1),
	Name nvarchar(50),
	Constraint [PK Campaign_Type] Primary Key(CampaignTypeId)
);

create table [dbo].[Campaign_Status](
	CampaignStatusId int IDENTITY(1, 1),
	Name nvarchar(50),
	Constraint [PK Campaign_Status] Primary Key(CampaignStatusId)
);

create table [dbo].[Campaign](
	CampaignId int IDENTITY(1, 1),
	UserId int,
	Name nvarchar(50),
	Active bit,
	CampaignTypeId int,
	CampaignStatusId int,
	StartDate date,
	EndDate date,
	ExpectedRevenue money,
	BudgetedCost money,
	ActualCost money,
	ExpectedResponse numeric(2,2),
	NumberSent int,
	CampaignParent int,
	[Description] nvarchar(max),
	CreateBy int,
	CreateDate datetime,
	UpdateBy int,
	UpdateDate datetime,
	ViewDate datetime,
	HiddenCampaign bit,
	Constraint [PK Campaign] Primary Key(CampaignId),
	Constraint [FK Campaign User] Foreign Key(UserId) References [dbo].[User](UserId),
	Constraint [FK Campaign User_Creator] Foreign Key(CreateBy) References [dbo].[User](UserId),
	Constraint [FK Campaign User_Modificator] Foreign Key(UpdateBy) References [dbo].[User](UserId),
	Constraint [FK Campaign Campaign_Parent] Foreign Key(CampaignParent) References [dbo].[Campaign](CampaignId),
	Constraint [FK Campaign Campaign_Type] Foreign Key(CampaignTypeId) References [dbo].[Campaign_Type](CampaignTypeId),
	Constraint [FK Campaign Campaign_Status] Foreign Key(CampaignStatusId) References [dbo].[Campaign_Status](CampaignStatusId)
);

---------
--Leads--
---------

create table [dbo].[Leads](
	LeadId int IDENTITY(1, 1),
	UserId int,
	Name nvarchar(50),
	LastName nvarchar(50),
	Company nvarchar(255),
	Title nvarchar(255),
	PhoneNumber nvarchar(20),
	HomePhoneNumber nvarchar(20),
	MobileNumber nvarchar(20),
	OtherPhoneMobile nvarchar(20),
	FaxNumber nvarchar(20),
	Email nvarchar(100),
	Employees int,
	[Description] nvarchar(max),
	LeadSourceId int,
	RatingId int,
	CampaignId int,
	IndustryId int,
	AddressId int,
	CreateBy int,
	CreateDate datetime,
	UpdateBy int,
	UpdateDate datetime,
	HiddenLead bit,
	ViewDate datetime,
	Constraint [PK Leads] Primary Key(LeadId),
	Constraint [FK Leads User] Foreign Key(UserId) References [dbo].[User](UserId),
	Constraint [FK Leads User_Creator] Foreign Key(CreateBy) References [dbo].[User](UserId),
	Constraint [FK Leads User_Modificator] Foreign Key(UpdateBy) References [dbo].[User](UserId),
	Constraint [FK Leads Lead_Source] Foreign Key(LeadSourceId) References [dbo].[Lead_Source](LeadSourceId),
	Constraint [FK Leads Industry] Foreign Key(IndustryId) References [dbo].[Industry](IndustryId),
	Constraint [FK Leads Address] Foreign Key(AddressId) References [dbo].[Address](AddressId),
	Constraint [FK Leads Rating] Foreign Key(RatingId) References [dbo].[Rating](RatingId),
	Constraint [FK Leads Campaign] Foreign Key(CampaignId) References [dbo].[Campaign](CampaignId)
);

-----------------
--Opportunities--
-----------------

create table [dbo].[Opportunities_Type](
	OpportunityTypeId int IDENTITY(1, 1),
	Name nvarchar(50),
	Constraint [PK Opportunities_Type] Primary Key(OpportunityTypeId)
);

create table [dbo].[Opportunities_Stage](
	OpportunityStageId int IDENTITY(1, 1),
	Name nvarchar(50),
	Probability int,
	Constraint [PK Opportunities_Stage] Primary Key(OpportunityStageId)
);

create table [dbo].[Competidors](
	CompetidorId int IDENTITY(1, 1),
	Name nvarchar(100),
	Strenght int,
	Weakness int,
	Constraint [PK Competidor] Primary Key(CompetidorId),
	Constraint [FK Competidor Industry_Strenght] Foreign Key(Strenght) References Industry(IndustryId),
	Constraint [FK Competidor Industry_Weakness] Foreign Key(Weakness) References Industry(IndustryId)
);

create table [dbo].[Opportunities_Status](
	OpportunityStatusId int IDENTITY(1, 1),
	Name nvarchar(50),
	Constraint [PK Opportunities_Status] Primary Key(OpportunityStatusId)
);

create table [dbo].[Opportunities](
	OpportunityId int IDENTITY(1, 1),
	UserId int,
	Name nvarchar(100),
	AccountId int,
	OpportunityTypeId int,
	[Description] nvarchar(max),
	Amount money,
	CloseDate date,
	NextStep nvarchar(max),
	OpportunityStageId int,	
	CampaignPrimarySourceId int,
	LeadSourceId int,
	OrderNumber nvarchar(255),
	CurrentGenerator nvarchar(255),
	TrackingNumber nvarchar(255),
	OpportunityStatusId int,
	CompetidorId int,
	ProductId int,
	CreateBy int,
	CreateDate datetime,
	UpdateBy int,
	UpdateDate datetime,
	Private bit,
	ViewDate datetime,
	HiddenOpportunity bit,
	Constraint [PK Opportunities] Primary Key(OpportunityId),
	Constraint [FK Opportunities User] Foreign Key(UserId) References [dbo].[User](UserId),
	Constraint [FK Opportunities User_Creator] Foreign Key(CreateBy) References [dbo].[User](UserId),
	Constraint [FK Opportunities User_Modificator] Foreign Key(UpdateBy) References [dbo].[User](UserId),
	Constraint [FK Opportunities Account] Foreign Key(AccountId) References [dbo].[Account](AccountId),
	Constraint [FK Opportunities Products] Foreign Key(ProductId) References [dbo].[Products](ProductId),
	Constraint [FK Opportunities Campaign] Foreign Key(CampaignPrimarySourceId) References [dbo].[Campaign](CampaignId),
	Constraint [FK Opportunities Opportunities_Type] Foreign Key(OpportunityTypeId) References [dbo].[Opportunities_Type](OpportunityTypeId),
	Constraint [FK Opportunities Opportunities_Stage] Foreign Key(OpportunityStageId) References [dbo].[Opportunities_Stage](OpportunityStageId),
	Constraint [FK Opportunities Competidor] Foreign Key(CompetidorId) References [dbo].[Competidors](CompetidorId),
	Constraint [FK Opportunities Opportunities_Status] Foreign Key(OpportunityStatusId) References [dbo].[Opportunities_Status](OpportunityStatusId),
	Constraint [FK Opportunities Lead_Source] Foreign Key(LeadSourceId) References [dbo].[Lead_Source](LeadSourceId)
);



------------
--Products--
------------

create table [dbo].[Products](
	ProductId int IDENTITY(1, 1),
	Name nvarchar(100),
	Code nvarchar(25),
	[Description] nvarchar(max),
	Active bit,
	Price money,
	Quantity int,
	CreateBy int,
	CreateDate datetime,
	UpdateBy int,
	UpdateDate datetime,
	ViewDate datetime,
	HiddenProduct bit,
	Constraint [PK Products] Primary Key(ProductId),
	Constraint [FK Products User_Creator] Foreign Key(CreateBy) References [dbo].[User](UserId),
	Constraint [FK Products User_Modificator] Foreign Key(UpdateBy) References [dbo].[User](UserId)
);

-------------
--Inventory--
-------------

create table [dbo].[Inventory](
	InventoryId int IDENTITY(1, 1),
	Name nvarchar(100),
	AddressId int,
	CreateBy int,
	CreateDate datetime,
	UpdateBy int,
	UpdateDate datetime,
	Constraint [PK Inventory] Primary Key(InventoryId),
	Constraint [FK Inventory Address] Foreign Key(AddressId) References [dbo].[Address](AddressId),
	Constraint [FK Inventory User_Creator] Foreign Key(CreateBy) References [dbo].[User](UserId),
	Constraint [FK Inventory User_Modificator] Foreign Key(UpdateBy) References [dbo].[User](UserId)
);

create table [dbo].[Inventory_Products](
	InventoryProductId int IDENTITY(1, 1),
	ProductId int,
	InventoryId int,
	Constraint [PK Inventory_Products] Primary Key(InventoryProductId),
	Constraint [FK Inventory_Products Products] Foreign Key(ProductId) References [dbo].[Products](ProductId),
	Constraint [FK Inventory_Products Inventory] Foreign Key(InventoryId) References [dbo].[Inventory](InventoryId)
);

---------
--Cases--
---------
create table [dbo].[Case_Status](
	CaseStatusId int IDENTITY(1, 1),
  	Name nvarchar(50),
  	Constraint [PK Case_Status] Primary Key(CaseStatusId)
);


create table [dbo].[Case_Type](
	CaseTypeId int IDENTITY(1, 1),
	Name nvarchar(50),
	Constraint [PK Case_Type] Primary Key(CaseTypeId)
);

create table [dbo].[Case_Reason](
	CaseReasonId int IDENTITY(1, 1),
	Name nvarchar(50),
	Constraint [PK Case_Reason] Primary Key(CaseReasonId)
);

create table [dbo].[Case_Origin](
	CaseOriginId int IDENTITY(1, 1),
	Name nvarchar(50),
	Constraint [PK Case_Origin] Primary Key(CaseOriginId)
);

create table [dbo].[Case_Priority](
	CasePriorityId int IDENTITY(1, 1),
	Name nvarchar(50),
	Constraint [PK Case_Priority] Primary Key(CasePriorityId)
);

create table [dbo].[Cases](
	CaseId int IDENTITY(1, 1),
	UserId int,
	ContactId int,
	AccountId int,
	CaseTypeId int,
	CaseReasonId int,
	CaseOriginId int,
	CaseStatusId int,
	CasePriorityId int,
	ProductId int,
	[Description] nvarchar(max),
	CreateBy int,
	CreateDate datetime,
	UpdateBy int,
	UpdateDate datetime,
	ViewDate datetime,
	HiddenCase bit,
	Constraint [PK Cases] Primary Key(CaseId),
	Constraint [FK Cases User] Foreign Key(UserId) References [dbo].[User](UserId),
	Constraint [FK Cases User_Creator] Foreign Key(CreateBy) References [dbo].[User](UserId),
	Constraint [FK Cases User_Modificator] Foreign Key(UpdateBy) References [dbo].[User](UserId),
	Constraint [FK Cases Contacts] Foreign Key(ContactId) References [dbo].[Contact](ContactId),
	Constraint [FK Cases Account] Foreign Key(AccountId) References [dbo].[Account](AccountId),
	Constraint [FK Cases Case_Type] Foreign Key(CaseTypeId) References [dbo].[Case_Type](CaseTypeId),
	Constraint [FK Cases Case_Reason] Foreign Key(CaseReasonId) References [dbo].[Case_Reason](CaseReasonId),
	Constraint [FK Cases Case_Origin] Foreign Key(CaseOriginId) References [dbo].[Case_Origin](CaseOriginId),
	Constraint [FK Cases Case_Status] Foreign Key(CaseStatusId) References [dbo].[Case_Status](CaseStatusId),
	Constraint [FK Cases Case_Priority] Foreign Key(CasePriorityId) References [dbo].[Case_Priority](CasePriorityId),
	Constraint [FK Cases Products] Foreign Key(ProductId) References [dbo].[Products](ProductId)
);


--Recently Added

Create Table [dbo].[Objects_ImgURL](
	ObjectImgurlId int IDENTITY(1, 1),
    ImgUrl nvarchar(50),
    Objectid int,
    constraint [PK Objects_ImgURL] Primary Key(ObjectImgurlId),
	constraint [FK Objects_ImgURL Objects] Foreign Key(objectid) References [dbo].[Objects](objectid)
);

Create Table [dbo].[Objects_Views](
	ObjectsViewsId int IDENTITY(1, 1),
	name nvarchar(50),
	objectid int,
	constraint [PK Objects_Views] Primary Key(ObjectsViewsId),
	constraint [FK Objects_Views Objects] Foreign Key(objectid) References [dbo].[Objects](objectid)
);

create table [dbo].[Records](
	RecordId int IDENTITY(1, 1),
	Place nvarchar(100),
	[Description] nvarchar(256),
	[Date] datetime,
	Constraint [PK Bitacora] Primary Key(RecordId)
);