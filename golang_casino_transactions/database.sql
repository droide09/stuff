-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Versione server:              10.6.3-MariaDB - mariadb.org binary distribution
-- S.O. server:                  Win64
-- HeidiSQL Versione:            11.3.0.6295
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- Dump della struttura del database testgo
CREATE DATABASE IF NOT EXISTS `testgo` /*!40100 DEFAULT CHARACTER SET latin1 */;
USE `testgo`;

-- Dump della struttura di tabella testgo.tutorials_tbl
CREATE TABLE IF NOT EXISTS `tutorials_tbl` (
  `tutorial_id` int(11) NOT NULL AUTO_INCREMENT,
  `tutorial_title` varchar(100) NOT NULL,
  `tutorial_author` varchar(40) NOT NULL,
  `submission_date` date DEFAULT NULL,
  PRIMARY KEY (`tutorial_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Dump dei dati della tabella testgo.tutorials_tbl: ~0 rows (circa)
DELETE FROM `tutorials_tbl`;
/*!40000 ALTER TABLE `tutorials_tbl` DISABLE KEYS */;
/*!40000 ALTER TABLE `tutorials_tbl` ENABLE KEYS */;

-- Dump della struttura di tabella testgo.users
CREATE TABLE IF NOT EXISTS `users` (
  `wallet_id` varchar(255) DEFAULT NULL,
  `password` varchar(255) DEFAULT NULL,
  `token` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Dump dei dati della tabella testgo.users: ~2 rows (circa)
DELETE FROM `users`;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` (`wallet_id`, `password`, `token`) VALUES
	('333', 'quick', 'fpllngzi'),
	('334', 'quick', 'eyoh43e0');
/*!40000 ALTER TABLE `users` ENABLE KEYS */;

-- Dump della struttura di tabella testgo.wallets
CREATE TABLE IF NOT EXISTS `wallets` (
  `wallet_id` varchar(255) NOT NULL,
  `Amount` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`wallet_id`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Dump dei dati della tabella testgo.wallets: ~6 rows (circa)
DELETE FROM `wallets`;
/*!40000 ALTER TABLE `wallets` DISABLE KEYS */;
INSERT INTO `wallets` (`wallet_id`, `Amount`) VALUES
	('100', '0'),
	('101', '0'),
	('102', '0'),
	('103', '0'),
	('333', '0.99'),
	('334', '91');
/*!40000 ALTER TABLE `wallets` ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
