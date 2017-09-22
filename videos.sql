-- phpMyAdmin SQL Dump
-- version 4.1.14
-- http://www.phpmyadmin.net
--
-- Host: 127.0.0.1
-- Generation Time: Oct 16, 2016 at 03:47 PM
-- Server version: 5.6.17
-- PHP Version: 5.5.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Database: `videos`
--

-- --------------------------------------------------------

--
-- Table structure for table `branch`
--

CREATE TABLE IF NOT EXISTS `branch` (
  `branch_no` int(11) NOT NULL AUTO_INCREMENT,
  `street` varchar(150) NOT NULL,
  `city` varchar(150) NOT NULL,
  `state` varchar(150) NOT NULL,
  `zip_code` varchar(50) NOT NULL,
  `tel_no` varchar(10) NOT NULL,
  PRIMARY KEY (`branch_no`),
  UNIQUE KEY `tel_no` (`tel_no`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=6 ;

--
-- Dumping data for table `branch`
--

INSERT INTO `branch` (`branch_no`, `street`, `city`, `state`, `zip_code`, `tel_no`) VALUES
(1, 'rizal street', 'bacoor city', 'cavite', '4102', '4346031'),
(2, 'glade street', 'bacoor', 'cavite', '4120', '434202'),
(3, 'global', 'lemon city', 'cavite', '4121', '43042'),
(4, '', '', '', '', ''),
(5, 'Maliban', 'Bacoor', 'Cavite', '4120', '420323');

-- --------------------------------------------------------

--
-- Table structure for table `customer`
--

CREATE TABLE IF NOT EXISTS `customer` (
  `customer_no` int(11) NOT NULL AUTO_INCREMENT,
  `fname` varchar(150) NOT NULL,
  `lname` varchar(150) NOT NULL,
  `address` varchar(150) NOT NULL,
  `registration_date` date NOT NULL,
  PRIMARY KEY (`customer_no`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=8 ;

--
-- Dumping data for table `customer`
--

INSERT INTO `customer` (`customer_no`, `fname`, `lname`, `address`, `registration_date`) VALUES
(1, 'bean', 'milo', 'rizal bacoor, cavite', '2016-09-24'),
(2, 'marc', 'blip', '120 street Singapore subdivision bacoor cavite', '2016-09-06'),
(3, 'sup', 'glade', 'amsdasfmamm', '2016-09-25'),
(4, 'jm', 'oiramla', 'kalayaan homes bacoor, cavite', '2016-10-16'),
(5, 'justin', 'almario', 'bacoor', '2016-10-16'),
(6, 'sup', 'dong', 'asdasdasdasd', '2016-10-16'),
(7, 'patrick', 'dwebs', '324 mabuhay street bacoor cavite', '2016-10-16');

-- --------------------------------------------------------

--
-- Table structure for table `staff`
--

CREATE TABLE IF NOT EXISTS `staff` (
  `staff_no` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(150) NOT NULL,
  `position` varchar(150) NOT NULL,
  `salary` float NOT NULL,
  PRIMARY KEY (`staff_no`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=4 ;

--
-- Dumping data for table `staff`
--

INSERT INTO `staff` (`staff_no`, `name`, `position`, `salary`) VALUES
(1, 'Justin Almario', 'Killing Operating Supervisior Officer', 10000000),
(2, 'Bean', 'Janitor', 15),
(3, 'John Cena', 'Janitor', 12);

-- --------------------------------------------------------

--
-- Table structure for table `stock`
--

CREATE TABLE IF NOT EXISTS `stock` (
  `catalog_no` int(11) NOT NULL AUTO_INCREMENT,
  `branch_no` int(11) DEFAULT NULL,
  `title` varchar(150) NOT NULL,
  `category` varchar(150) NOT NULL,
  `cost` float NOT NULL,
  `cast` varchar(150) NOT NULL,
  `director` varchar(150) NOT NULL,
  PRIMARY KEY (`catalog_no`),
  KEY `branch_no` (`branch_no`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=7 ;

--
-- Dumping data for table `stock`
--

INSERT INTO `stock` (`catalog_no`, `branch_no`, `title`, `category`, `cost`, `cast`, `director`) VALUES
(1, 1, 'Harry Potter 1', 'Fantasy', 150, 'Danielle Radcliffe, Emma Watson', 'aosdkaksdmam'),
(2, 2, 'Skyrim', 'Sci-Fi', 120, 'asodmaosdm', 'osdkaoskfamm'),
(3, 1, 'Cabin In The Woods', 'Sci-Fi, Horror', 170, 'mgamamasdo', 'aosdkasdoaoo'),
(4, 3, 'OnePuchMan', 'ecchi', 11, 'saitama,genos ', 'oneman'),
(5, 3, 'Sword Art Online', 'romance,drama,adventure,ecchi', 9, 'kirito,asuna', 'dong gae'),
(6, 4, 'Dracula', 'Romance', 12, 'Blade,Pooh', 'John Cena');

-- --------------------------------------------------------

--
-- Table structure for table `video_copy`
--

CREATE TABLE IF NOT EXISTS `video_copy` (
  `video_no` int(11) NOT NULL AUTO_INCREMENT,
  `catalog_no` int(11) DEFAULT NULL,
  `status` varchar(50) NOT NULL,
  PRIMARY KEY (`video_no`),
  KEY `catalog_no` (`catalog_no`),
  KEY `catalog_no_2` (`catalog_no`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=5 ;

--
-- Dumping data for table `video_copy`
--

INSERT INTO `video_copy` (`video_no`, `catalog_no`, `status`) VALUES
(1, 1, 'Available'),
(2, 2, 'Rented'),
(3, 1, 'Rented'),
(4, 2, 'Rented');

-- --------------------------------------------------------

--
-- Table structure for table `video_rental`
--

CREATE TABLE IF NOT EXISTS `video_rental` (
  `rental_no` int(11) NOT NULL AUTO_INCREMENT,
  `customer_no` int(11) DEFAULT NULL,
  `video_no` int(11) DEFAULT NULL,
  `daily_rent` float NOT NULL,
  `rent_date` date NOT NULL,
  `return_date` date NOT NULL,
  PRIMARY KEY (`rental_no`),
  KEY `customer_no` (`customer_no`,`video_no`),
  KEY `video_no` (`video_no`),
  KEY `daily_rent` (`daily_rent`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=5 ;

--
-- Dumping data for table `video_rental`
--

INSERT INTO `video_rental` (`rental_no`, `customer_no`, `video_no`, `daily_rent`, `rent_date`, `return_date`) VALUES
(1, 2, 2, 150, '2016-09-24', '2016-09-30'),
(4, 3, 4, 120, '2016-09-26', '2016-09-29');

--
-- Constraints for dumped tables
--

--
-- Constraints for table `stock`
--
ALTER TABLE `stock`
  ADD CONSTRAINT `stock_ibfk_1` FOREIGN KEY (`branch_no`) REFERENCES `branch` (`branch_no`) ON UPDATE CASCADE;

--
-- Constraints for table `video_copy`
--
ALTER TABLE `video_copy`
  ADD CONSTRAINT `video_copy_ibfk_1` FOREIGN KEY (`catalog_no`) REFERENCES `stock` (`catalog_no`) ON UPDATE CASCADE;

--
-- Constraints for table `video_rental`
--
ALTER TABLE `video_rental`
  ADD CONSTRAINT `video_rental_ibfk_2` FOREIGN KEY (`video_no`) REFERENCES `video_copy` (`video_no`) ON UPDATE CASCADE,
  ADD CONSTRAINT `video_rental_ibfk_1` FOREIGN KEY (`customer_no`) REFERENCES `customer` (`customer_no`) ON UPDATE CASCADE;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
