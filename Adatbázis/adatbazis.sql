DROP DATABASE IF EXISTS `mydrivers`;
CREATE DATABASE IF NOT EXISTS `mydrivers`
DEFAULT CHARACTER SET utf8
COLLATE utf8_hungarian_ci;

USE `mydrivers`;

CREATE TABLE `Jarmu` (
	`jid` INT NOT NULL AUTO_INCREMENT,
	`rendszam` varchar(16) NOT NULL,
	`evjarat` varchar(9) NOT NULL,
	`gyarto` varchar(16) NOT NULL,
	`modell` varchar(64) NOT NULL,
	`kmora` INT NOT NULL,
	PRIMARY KEY (`jid`)
);

CREATE TABLE `Szerviz` (
	`szid` INT NOT NULL AUTO_INCREMENT,
	`cim` varchar(256) NOT NULL,
	`sznev` varchar(64) NOT NULL,
	`kontaktnev` varchar(64) NOT NULL,
	`kontaktemail` varchar(256),
	`kontakttelszam` varchar(16),
	PRIMARY KEY (`szid`)
);

CREATE TABLE `Munkakor` (
	`mkid` INT NOT NULL AUTO_INCREMENT,
	`mknev` varchar(32) NOT NULL UNIQUE,
	PRIMARY KEY (`mkid`)
);

CREATE TABLE `Jogosultsag`(
	`jogid` INT NOT NULL AUTO_INCREMENT,
	`jognev` VARCHAR(32) NOT NULL UNIQUE,
	PRIMARY KEY (`jogid`)
);

CREATE TABLE `Felhasznalo` (
	`fid` INT NOT NULL AUTO_INCREMENT,
	`munkakorid` INT NOT NULL,
	`jogid` INT NOT NULL,
	`inaktiv` BOOLEAN NOT NULL DEFAULT 0,
	`email` varchar(256),
	`fnev` varchar(32) NOT NULL,
	`jelszo` varchar(256) NOT NULL,
	`szuletesnap` DATE,
	`kivehetoszabi` INT,
	`kivettszabi` INT,
	`nem` varchar(32),
	`teljesnev` varchar(64),
	`telefonszam` varchar(16),
	`lakcim` varchar(64),
	`adoazon` varchar(11),
	`bankszamlaszam` varchar(24),
	`szemelyi` varchar(11),
	`tajszam` varchar(9),
	`csatlakozas` DATE,
	`alapber` int,
	PRIMARY KEY (`fid`),
	FOREIGN KEY (`munkakorid`) REFERENCES `Munkakor`(`mkid`),
	FOREIGN KEY (`jogid`) REFERENCES `Jogosultsag`(`jogid`)
);

CREATE TABLE `Szervizeles` (
	`szerid` INT NOT NULL AUTO_INCREMENT,
	`jid` INT NOT NULL,
	`szid` INT NOT NULL,
	`mettol` DATE NOT NULL,
	`meddig` DATE NOT NULL,
	`indok` varchar(512) NOT NULL,
	PRIMARY KEY (`szerid`),
	FOREIGN KEY (`jid`) REFERENCES `Jarmu`(`jid`),
	FOREIGN KEY (`szid`) REFERENCES `Szerviz`(`szid`)
);

CREATE TABLE `Beosztas` (
	`bid` INT NOT NULL AUTO_INCREMENT,
	`fid` INT NOT NULL,
	`jid` INT NOT NULL,
	`datum` DATE NOT NULL,
	`muszak` BOOLEAN NOT NULL,
	`ora` INT NOT NULL,
	PRIMARY KEY (`bid`),
	FOREIGN KEY (`fid`) REFERENCES `Felhasznalo`(`fid`),
	FOREIGN KEY (`jid`) REFERENCES `Jarmu`(`jid`)
);

CREATE TABLE `Keruletek`(
	`kerid` INT NOT NULL,
	`kernev` varchar(64) NOT NULL UNIQUE,
	PRIMARY KEY (`kerid`)
);

CREATE TABLE `Fuvar` (
	`fuvar_id` INT NOT NULL AUTO_INCREMENT,
	`fid` INT NOT NULL,
	`jid` INT NOT NULL,
	`kerid` INT NOT NULL,
	`datum` DATE,
	`kezdes` varchar(5),
	`befejezes` varchar(5),
	`felvett_csom` INT NOT NULL,
	`sikeres_csom` INT,
	PRIMARY KEY (`fuvar_id`),
	FOREIGN KEY (`fid`) REFERENCES `Felhasznalo`(`fid`),
	FOREIGN KEY (`jid`) REFERENCES `Jarmu`(`jid`),
	FOREIGN KEY (`kerid`) REFERENCES `Keruletek`(`kerid`)
);

INSERT INTO Munkakor (Munkakor.mkid, Munkakor.mknev)
VALUES(1, "Admin");

INSERT INTO Jogosultsag (Jogosultsag.jognev)
VALUES("Felhasználó"),("Operátor"),("Admin");

INSERT INTO Felhasznalo(Felhasznalo.munkakorid, Felhasznalo.jogid, Felhasznalo.fnev, Felhasznalo.jelszo)
VALUES(
	(SELECT Munkakor.mkid FROM Munkakor WHERE Munkakor.mknev LIKE "Admin"),
	(SELECT Jogosultsag.jogid FROM Jogosultsag WHERE Jogosultsag.jognev LIKE "Admin"),
	 "admin", 
	 SHA2("admin",256)
	 );

INSERT INTO Keruletek(Keruletek.kerid, Keruletek.kernev)
VALUES(1,"I.Kerület - Budavár"),(2,"II.Kerület"),(3,"III.Kerület - Óbuda/Békásmegyer"),
(4,"IV.Kerület - Újpest"),(5,"V.Kerület - Belváros/Lipótváros"),(6,"VI.Kerület - Terézváros"),
(7,"VII.Kerület - Erzsébetváros"),(8,"VIII.Kerület - Józsefváros"),(9,"IX.Kerület - Ferencváros"),
(10,"X.Kerület - Kőbánya"),(11,"XI.Kerület - Újbuda"),(12,"XII.Kerület - Hegyvidék"),
(13,"XIII.Kerület"),(14,"XIV.Kerület - Zugló"),(15,"XV.Kerület - Rákospalota/Pestújhely/Újpalota"),
(16,"XVI.Kerület"),(17,"XVII.Kerület - Rákosmente"),(18,"XVIII.Kerület - Pestszentlőrinc/Pestszentimre"),
(19,"XIX.Kerület - Kispest"),(20,"XX.Kerület - Pesterzsébet"),(21,"XXI.Kerület - Csepel"),
(22,"XXII.Kerület - Budafok-Tétény"),(23,"XXIII.Kerület - Soroksár");