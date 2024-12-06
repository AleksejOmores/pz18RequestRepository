create database regApplication;

use regApplication;

CREATE TABLE Clients (    
ClientID INT not null PRIMARY KEY,
    FullName NVARCHAR(100) NOT NULL,    
	PhoneNumber NVARCHAR(20) NOT NULL
);

-- ������� ��������
CREATE TABLE Technicians (
    TechnicianID INT not null PRIMARY KEY,    
	FullName NVARCHAR(100) NOT NULL,
    Specialization NVARCHAR(100) NOT NULL);

-- ���������� ������� �������� ������

CREATE TABLE RequestStatus (    
	StatusID INT not null PRIMARY KEY,
    StatusName NVARCHAR(50) NOT NULL UNIQUE);

-- ���������� ������� ����� ���������
CREATE TABLE DeviceTypes (    
	DeviceTypeID INT not null PRIMARY KEY,
    DeviceTypeName NVARCHAR(50) NOT NULL UNIQUE);

-- ����� ������� ������� ���������
CREATE TABLE DeviceModels (    DeviceModelID INT not null PRIMARY KEY,
    DeviceModelName NVARCHAR(100) NOT NULL,    DeviceTypeID INT NOT NULL,
    FOREIGN KEY (DeviceTypeID) REFERENCES DeviceTypes(DeviceTypeID));

-- ������� ������
CREATE TABLE Requests (
    RequestID INT not null PRIMARY KEY,    DateAdded DATE NOT NULL,
    DeviceModelID INT NOT NULL,    ProblemDescription NVARCHAR(255) NOT NULL,
    StatusID INT NOT NULL,    ClientID INT NOT NULL,
    TechnicianID INT NULL,
    FOREIGN KEY (ClientID) REFERENCES Clients(ClientID),    FOREIGN KEY (TechnicianID) REFERENCES Technicians(TechnicianID),
    FOREIGN KEY (DeviceModelID) REFERENCES DeviceModels(DeviceModelID),    FOREIGN KEY (StatusID) REFERENCES RequestStatus(StatusID)
);

-- ������� ������������
CREATE TABLE Comments (
    CommentID INT not null PRIMARY KEY,    RequestID INT NOT NULL,
    CommentText NVARCHAR(500) NOT NULL,    CommentDate DATETIME NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (RequestID) REFERENCES Requests(RequestID));

INSERT INTO Clients (ClientID, FullName, PhoneNumber) VALUES 
(1,'������ ���� ��������', '+7 (900) 123-45-67'),
(2,'������� ���� ���������', '+7 (901) 234-56-78'),
(3,'������� ������� ��������', '+7 (902) 345-67-89'),
(4,'��������� ����� ����������', '+7 (903) 456-78-90');

INSERT INTO Technicians (TechnicianID, FullName, Specialization) 
VALUES (1,'������� ������� ��������', '������ ���������� �����'),
(2,'�������� ���� ����������', '������ �������������'),
(3,'������� ����� ��������', '������ �����������'),
(4,'������� ������ ��������', '������ ���������');

INSERT INTO RequestStatus (StatusID, StatusName) VALUES
(1,'����� ������'),
(2,'� �������� �������'),
(3,'������ � ������'),
(4,'�������� ���������');

INSERT INTO DeviceTypes (DeviceTypeID, DeviceTypeName) VALUES
(1,'���������� ������'),
(2,'�����������'),
(3,'���������'),
(4,'�������');

INSERT INTO DeviceModels (DeviceModelID, DeviceModelName, DeviceTypeID) 
VALUES
(1,'Samsung WW80K5210YW', 1),  -- ���������� ������
(2,'LG GA-B459', 2),           -- �����������
(3,'Sony Bravia KDL-43WF665', 3), -- ���������
(4,'Bosch BGL35MOV21', 4);     -- �������

INSERT INTO Requests (RequestID, DateAdded, DeviceModelID, ProblemDescription, StatusID, ClientID, TechnicianID) VALUES 
(1,'2024-01-10', 1, '�� ����������� ���������� ������', 1, 1, 1), -- ����� ������
(2,'2024-01-12', 2, '����������� �� ���������', 2, 2, 2),          -- � �������� �������
(3,'2024-01-15', 3, '��� ����������� �� ������ ����������', 3, 3, 1), -- ������ � ������
(4,'2024-01-18', 4, '������� �� ��������� ����', 1, 4, 2);          -- ����� ������

INSERT INTO Comments (CommentID, RequestID, CommentText) 
VALUES (1,1, '��������� �����������. ��������� ������ ������.'),
(2,2, '���������� ������ �����������. �������� ��������.'),
(3,3, '��������� ��������������. ��������� ������������.'),
(4,4, '�������� � �������. ��������� ������������� ������� �� �������.');

select * from Requests