USE DB53

ALTER TABLE Person ADD CONSTRAINT CHK_Discrim CHECK ( Discriminator IN ('Admin','Staff','Customer'))

ALTER TABLE Person ADD CONSTRAINT CHK_Gen CHECK ( Gender IN ('M','F'))
