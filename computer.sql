-- phpMyAdmin SQL Dump
-- version 5.1.1
-- https://www.phpmyadmin.net/
--
-- Gép: 127.0.0.1
-- Létrehozás ideje: 2024. Nov 19. 13:29
-- Kiszolgáló verziója: 10.4.20-MariaDB
-- PHP verzió: 7.3.29

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Adatbázis: `computer`
--
CREATE DATABASE IF NOT EXISTS `computer` DEFAULT CHARACTER SET utf8 COLLATE utf8_hungarian_ci;
USE `computer`;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `comp`
--

CREATE TABLE `comp` (
  `Id` char(36) COLLATE utf8_hungarian_ci NOT NULL,
  `Brand` varchar(37) COLLATE utf8_hungarian_ci NOT NULL,
  `Type` varchar(30) COLLATE utf8_hungarian_ci NOT NULL,
  `Display` double NOT NULL,
  `Memory` int(11) NOT NULL,
  `CreatedTime` datetime NOT NULL,
  `OsId` char(36) COLLATE utf8_hungarian_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `os`
--

CREATE TABLE `os` (
  `Id` char(36) COLLATE utf8_hungarian_ci NOT NULL,
  `Name` varchar(27) COLLATE utf8_hungarian_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

--
-- A tábla adatainak kiíratása `os`
--

INSERT INTO `os` (`Id`, `Name`) VALUES
('3b2c5a98-3849-445e-a7e4-7e133e7c37b7', 'Windows 7'),
('61700462-804a-47bf-b9e0-2a7b0ba1cc6d', 'FreeDos'),
('7f3196da-0be6-4773-a8ef-0d74e3f1d5b9', 'Linux'),
('d6247b4b-599f-4932-b511-e019f4715350', 'Microsoft Vista');

--
-- Indexek a kiírt táblákhoz
--

--
-- A tábla indexei `comp`
--
ALTER TABLE `comp`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `OsId` (`OsId`);

--
-- A tábla indexei `os`
--
ALTER TABLE `os`
  ADD PRIMARY KEY (`Id`);

--
-- Megkötések a kiírt táblákhoz
--

--
-- Megkötések a táblához `comp`
--
ALTER TABLE `comp`
  ADD CONSTRAINT `comp_ibfk_1` FOREIGN KEY (`OsId`) REFERENCES `os` (`Id`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
