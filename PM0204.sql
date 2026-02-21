CREATE DATABASE EquipmentServiceDesk;
GO
USE EquipmentServiceDesk;

insert into Equipment ([Name], InventoryNumber, [Location])
values ('Рабочий компьютер №1', 'INV-001', 'Кабинет 101')


INSERT INTO Users (Username, PasswordHash, Role)
VALUES (
'admin',
'XohImNooBHFR0OVvjcYpJ3rj3V4w0uF9IYfYBf2o8YQ=',
'Admin'
);