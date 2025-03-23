create table Law_Enforcement_Agencies (
    Agency_Id int primary key identity(1,1),
    Agency_Name varchar(50) not null,
    Jurisdiction varchar(50) not null,
    Contact_Info varchar(50)
);

create table Officers (
    Officer_Id int primary key identity(1,1),
    First_Name varchar(50) not null,
    Last_Name varchar(50) not null,
    Badge_Number varchar(50) unique not null,
    Rank varchar(50) not null,
    Contact_Info nvarchar(500),
    Agency_Id int not null,
    Foreign Key (Agency_id) references Law_Enforcement_Agencies(Agency_Id) on delete cascade
);

create table Victims (
    Victim_Id int primary key identity(1,1),
    First_Name varchar(50) not null,
    Last_Name varchar(50) not null,
    Date_Of_Birth date not null,
    Gender varchar(10) check (gender in ('male', 'female', 'other')),
    Contact_Info nvarchar(500)
);

create table Suspects (
    Suspect_Id int primary key identity(1,1),
    First_Name varchar(50) not null,
    Last_Name nvarchar(50) not null,
    Date_Of_Birth date not null,
    Gender nvarchar(10) check (gender in ('male', 'female', 'other')),
    Contact_info varchar(50)
);

create table Incidents (
    Incident_Id int primary key identity(1,1),
    Incident_Type nvarchar(100) not null,
    Incident_Date datetime not null default getdate(),
    Location Geography not null, -- for storing latitude & longitude
    Description nvarchar(max),
    Status nvarchar(50) check (status in ('open', 'closed', 'under investigation')),
    Victim_Id int not null,
    Suspect_Id int not null,
    Agency_Id int not null,
    foreign key (Victim_Id) references Victims(Victim_Id) ,
    foreign key (suspect_id) references suspects(suspect_id) ,
    foreign key (agency_id) references law_enforcement_agencies(agency_id)
);

create table Evidence (
    Evidence_Id int primary key identity(1,1),
    Description nvarchar(max) not null,
    Location_Found nvarchar(255) not null,
    Incident_Id int not null,
    foreign key (Incident_Id) references Incidents(Incident_Id)
);

create table Reports (
    Report_Id int primary key identity(1,1),
    Incident_Id int not null,
    Reporting_Officer int not null,
    Report_Date datetime not null default getdate(),
    Report_Details nvarchar(max),
    Status nvarchar(50) check (status in ('draft', 'finalized')),
    foreign key (Incident_Id) references incidents(Incident_Id) ,
    foreign key (Reporting_Officer) references Officers(Officer_Id)
);



