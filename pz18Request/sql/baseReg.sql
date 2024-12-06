create database regApplication;

use regApplication;

CREATE TABLE Clients (    
ClientID INT not null PRIMARY KEY,
    FullName NVARCHAR(100) NOT NULL,    
	PhoneNumber NVARCHAR(20) NOT NULL
);

-- Таблица мастеров
CREATE TABLE Technicians (
    TechnicianID INT not null PRIMARY KEY,    
	FullName NVARCHAR(100) NOT NULL,
    Specialization NVARCHAR(100) NOT NULL);

-- Справочная таблица статусов заявок

CREATE TABLE RequestStatus (    
	StatusID INT not null PRIMARY KEY,
    StatusName NVARCHAR(50) NOT NULL UNIQUE);

-- Справочная таблица типов устройств
CREATE TABLE DeviceTypes (    
	DeviceTypeID INT not null PRIMARY KEY,
    DeviceTypeName NVARCHAR(50) NOT NULL UNIQUE);

-- Новая таблица моделей устройств
CREATE TABLE DeviceModels (    DeviceModelID INT not null PRIMARY KEY,
    DeviceModelName NVARCHAR(100) NOT NULL,    DeviceTypeID INT NOT NULL,
    FOREIGN KEY (DeviceTypeID) REFERENCES DeviceTypes(DeviceTypeID));

-- Таблица заявок
CREATE TABLE Requests (
    RequestID INT not null PRIMARY KEY,    DateAdded DATE NOT NULL,
    DeviceModelID INT NOT NULL,    ProblemDescription NVARCHAR(255) NOT NULL,
    StatusID INT NOT NULL,    ClientID INT NOT NULL,
    TechnicianID INT NULL,
    FOREIGN KEY (ClientID) REFERENCES Clients(ClientID),    FOREIGN KEY (TechnicianID) REFERENCES Technicians(TechnicianID),
    FOREIGN KEY (DeviceModelID) REFERENCES DeviceModels(DeviceModelID),    FOREIGN KEY (StatusID) REFERENCES RequestStatus(StatusID)
);

-- Таблица комментариев
CREATE TABLE Comments (
    CommentID INT not null PRIMARY KEY,    RequestID INT NOT NULL,
    CommentText NVARCHAR(500) NOT NULL,    CommentDate DATETIME NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (RequestID) REFERENCES Requests(RequestID));

INSERT INTO Clients (ClientID, FullName, PhoneNumber) VALUES 
(1,'Иванов Иван Иванович', '+7 (900) 123-45-67'),
(2,'Петрова Анна Сергеевна', '+7 (901) 234-56-78'),
(3,'Сидоров Дмитрий Олегович', '+7 (902) 345-67-89'),
(4,'Кузнецова Мария Викторовна', '+7 (903) 456-78-90');

INSERT INTO Technicians (TechnicianID, FullName, Specialization) 
VALUES (1,'Смирнов Алексей Павлович', 'Ремонт стиральных машин'),
(2,'Васильев Олег Николаевич', 'Ремонт холодильников'),
(3,'Зайцева Елена Петровна', 'Ремонт телевизоров'),
(4,'Морозов Виктор Семёнович', 'Ремонт пылесосов');

INSERT INTO RequestStatus (StatusID, StatusName) VALUES
(1,'Новая заявка'),
(2,'В процессе ремонта'),
(3,'Готова к выдаче'),
(4,'Ожидание запчастей');

INSERT INTO DeviceTypes (DeviceTypeID, DeviceTypeName) VALUES
(1,'Стиральная машина'),
(2,'Холодильник'),
(3,'Телевизор'),
(4,'Пылесос');

INSERT INTO DeviceModels (DeviceModelID, DeviceModelName, DeviceTypeID) 
VALUES
(1,'Samsung WW80K5210YW', 1),  -- Стиральная машина
(2,'LG GA-B459', 2),           -- Холодильник
(3,'Sony Bravia KDL-43WF665', 3), -- Телевизор
(4,'Bosch BGL35MOV21', 4);     -- Пылесос

INSERT INTO Requests (RequestID, DateAdded, DeviceModelID, ProblemDescription, StatusID, ClientID, TechnicianID) VALUES 
(1,'2024-01-10', 1, 'Не запускается стиральная машина', 1, 1, 1), -- Новая заявка
(2,'2024-01-12', 2, 'Холодильник не охлаждает', 2, 2, 2),          -- В процессе ремонта
(3,'2024-01-15', 3, 'Нет изображения на экране телевизора', 3, 3, 1), -- Готова к выдаче
(4,'2024-01-18', 4, 'Пылесос не всасывает пыль', 1, 4, 2);          -- Новая заявка

INSERT INTO Comments (CommentID, RequestID, CommentText) 
VALUES (1,1, 'Проведена диагностика. Требуется замена насоса.'),
(2,2, 'Необходима замена компрессора. Запчасть заказана.'),
(3,3, 'Телевизор отремонтирован. Проведено тестирование.'),
(4,4, 'Проблема в фильтре. Ожидается подтверждение ремонта от клиента.');

select * from Requests