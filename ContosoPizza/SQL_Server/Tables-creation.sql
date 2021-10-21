
create table LoanCategory
(
    LoanCategoryID int identity
        constraint PK_LoanCategory_1
            primary key,
    Description    varchar(50)
)
go

create table LoanType
(
    LoanTypeID         int identity
        constraint PK_LoanType
            primary key,
    LoanCategoryID     int                        not null
        constraint FK_LoanType_LoanCategory
            references LoanCategory,
    Description        varchar(50),
    LongDescription    varchar(255),
    Active             bit
        constraint DF_LoanType_IsActive default 1 not null,
    ActiveEndDate      date,
    RateLockDays       int                        not null,
    ActiveRates        bit                        not null,
    LoanTypeCategoryID int,
    SortOrder          decimal(5, 2),
    ProductTypeID      int
)
go

create table Loan
(
    LoanID                        int identity
        constraint PK_Loan
            primary key,
    LoanTypeID                    int
        constraint FK_Loan_LoanType
            references LoanType,
    LoanAmount                    decimal(12, 2),
    AnnualIncome                  decimal(12, 2),
    HouseholdCount                int,
    DeliveryDate                  date,
    LVRatio                       decimal(4, 2),
    InterestRate                  decimal(6, 5),
    Balance                       decimal(12, 2),
    InsCode                       int,
    InsurerNum                    varchar(20),
    ReservDateTime                datetime
)
go


create table StatusCode
(
    StatusCode             int not null
        constraint PK_StatusCode
            primary key,
    Description            varchar(255),
    BusinessUnit           varchar(50),
    NotesAndAssumptions    varchar(640),
    ConversationCategoryID int
)
go

create table LoanStatus
(
    LoanStatusID   int identity
        constraint PK_StatusHistory
            primary key,
    LoanID         int not null
        constraint FK_LoanStatus_Loan
            references Loan,
    StatusCode     int
        constraint FK_LoanStatus_StatusCode
            references StatusCode,
    StatusDate     datetime,
    LoginName      varchar(50),
    LoginDate      datetime,
    StatusSequence int
)
go


