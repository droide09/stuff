package walletmanager

import (
  "github.com/shopspring/decimal"
  "example/hello/logmanager" 
)

// manages the business logic
// the amount parameter is string and amount in the database is strings
// here the strings are converted to decimal to make calculations, the reconverted to string 

//
//	WmGetBalance
//
//  return the balance
//
func WmGetBalance(istore IDatastore,ilog logmanager.ILog,walletid string) (amount string,err string) {
	ilog.WriteLog("WmGetBalance",0,"params: "+walletid)
	amount,locError :=	istore.Get(walletid)
	ilog.WriteLogReturn("WmGetBalance",-1,"return: "+amount+" "+locError,locError)
	return amount,locError;
}

//
//	WmCredit
//
//  get the current balance
//  add the credit amount
//  write the new credit in the database
//  here also check for negative credit amount
//  this operation should be atomic
//
func WmCredit(istore IDatastore,ilog logmanager.ILog,walletid string,amount string) (err string) {
	ilog.WriteLog("WmCredit",0,"params "+walletid+" "+amount)
	locError := ""
    strAmount,err := istore.Get(walletid)
	if (err == "OK"){
		newAmount,intErr := decimal.NewFromString(strAmount)
		if (intErr == nil){
			amountToAdd,intErrA := decimal.NewFromString(amount)
			if (intErrA == nil){
				if (amountToAdd.Sign() < 0){
					locError = "Invalid negative credit"
				}else{
					newAmount = newAmount.Add(amountToAdd)
					locError = istore.Set(walletid,newAmount.String())
				}
				ilog.WriteLogReturn("WmCredit",-1,"return "+locError,locError)
				return locError
			}else{
				ilog.WriteLogReturn("WmCredit",-1,"return "+intErrA.Error(),intErrA.Error())
				return intErrA.Error()
			}
		}else{
			err = intErr.Error()
		}
	}
	ilog.WriteLogReturn("WmCredit",-1,"return "+err,err)
	return err
}

//
//	WmCredit
//
//  get the current balance
//  subtract the debit amount
//  write the new credit in the database
//  here also check for negative debit amount and if amount is available
//  this operation should be atomic
//
func WmDebit(istore IDatastore,ilog logmanager.ILog,walletid string,amount string) (err string) {
    ilog.WriteLog("WmDebit",0,"params "+walletid)
	locerr := ""
	strAmount,err := istore.Get(walletid)
	if (err == "OK"){
		newAmount,intErr := decimal.NewFromString(strAmount)
		if (intErr == nil){
			amountToSub,intErrA := decimal.NewFromString(amount)
			if (intErrA == nil){
				if (amountToSub.Sign() < 0){
					locerr = "Invalid negative debit"
				}else{
					newAmount = newAmount.Sub(amountToSub)
					if (newAmount.Sign() < 0){
						locerr = "No credit"
					} else {
						locerr = istore.Set(walletid,newAmount.String())			
					}
				}
				ilog.WriteLogReturn("WmDebit",-1,"return "+locerr,locerr)
				return locerr
			}else{
				ilog.WriteLogReturn("WmDebit",-1,"return "+intErrA.Error(),intErrA.Error())
				return intErrA.Error()
			}
		}else{
			err = intErr.Error()
		}
	}
	ilog.WriteLogReturn("WmDebit",-1,"return "+err,err)
	return err
}