USE DB53

ALTER TABLE Sold ADD CONSTRAINT CHK_ CHECK ( Discriminator IN ('Admin','Staff','Customer'))

select * from Sold