package main

import "testing"
import "example/hello/walletmanager"
import "example/hello/logmanager"

//
// unit test
// you have to reset the database to reexecute the tests
// UPDATE wallets SET amount='0' WHERE wallet_id IN (100,101,102)
//
// To execute the tests:
// go test
//

//
//  TestBalance
//
//  get the initial balance and check if 0
//  add 100 and check if 100
//  walletid is 100
//
func TestBalance(t *testing.T){
    var log logmanager.NullLog
	var ilog logmanager.ILog
	ilog = log
	
//	var store walletmanager.RedisStore
	var store walletmanager.MysqlStore
	var istore walletmanager.IDatastore
    istore = store

	// test balance 0
	balance,err := walletmanager.WmGetBalance(istore,ilog,"100")		
	if (err!="OK") {
		t.Errorf("walletmanager.WmGetBalance return error %s", err)
	}
	if (balance!="0") {
		t.Errorf("walletmanager.WmGetBalance return unespected value %s instead of 0", balance)
	}
	err = walletmanager.WmCredit(istore,ilog,"100","100")
	if (err!="OK") {
		t.Errorf("walletmanager.WmCredit return error %s", err)
	}
	balance,err = walletmanager.WmGetBalance(istore,ilog,"100")		
	if (err!="OK") {
		t.Errorf("walletmanager.WmGetBalance return error %s", err)
	}
	if (balance!="100") {
		t.Errorf("walletmanager.WmGetBalance return unespected value %s instead of 100", balance)
	}
}

//
//  TestBalance
//
//  get the initial balance and check if 0
//  add 7 credits and check if the balance is correct each time
//  walletid is 101
//
func TestCredit(t *testing.T){
    var log logmanager.NullLog
	var ilog logmanager.ILog
	ilog = log
	
//	var store walletmanager.RedisStore
	var store walletmanager.MysqlStore
	var istore walletmanager.IDatastore
    istore = store

	testAmounts := [7]string{"100", "0", "10000000","0.1","0.0001","12.34","-100"}
	expectedAmounts := [7]string{"100", "100", "10000100","10000100.1","10000100.1001","10000112.4401","10000112.4401"}

	balance,err := walletmanager.WmGetBalance(istore,ilog,"101")		
	if (err!="OK") {
		t.Errorf("walletmanager.WmGetBalance return error %s", err)
	}
	if (balance!="0") {
		t.Errorf("starting balance is %s, cannot execute the test, it must be zero", balance)
		return;
	}
	
	for i:=0;i<len(testAmounts);i++ {
		// test credit
		err = walletmanager.WmCredit(istore,ilog,"101",testAmounts[i])
		if ((err!="OK")&&(testAmounts[i]!="-100")){
			t.Errorf("walletmanager.WmCredit return error %s", err)
		}
		if (testAmounts[i]=="-100"){
			if (err !="Invalid negative credit"){
				t.Errorf("walletmanager.WmCredit return unexpected error for negative credit %s", err)
			}
		}
		balance,err := walletmanager.WmGetBalance(istore,ilog,"101")	
		if (err!="OK") {
			t.Errorf("walletmanager.WmGetBalance return error %s", err)
		}
		if (balance!=expectedAmounts[i]) {
			t.Errorf("walletmanager.WmGetBalance return unespected value %s instead of %s", balance,expectedAmounts[i])
		}
	}
}

//
//  TestBalance
//
//  get the initial balance and check if 0
//  add 10000112.4401
//  add 7 debits and check if the balance is correct each time
//  walletid is 102
//
func TestDebit(t *testing.T){
    var log logmanager.NullLog
	var ilog logmanager.ILog
	var istore walletmanager.IDatastore
	ilog = log
	
//	var store walletmanager.RedisStore
	var store walletmanager.MysqlStore
    istore = store

	// for debit,these array are reversed
	testAmounts := [7]string{"102", "0", "10000000","0.1","0.0001","12.34","-100"}
	expectedAmounts := [7]string{"100", "100", "10000100","10000100.1","10000100.1001","10000112.4401","10000112.4401"}

	balance,err := walletmanager.WmGetBalance(istore,ilog,"102")		
	if (err!="OK") {
		t.Errorf("walletmanager.WmGetBalance return error %s", err)
	}
	if (balance!="0") {
		t.Errorf("starting balance is %s, cannot execute the test, it must be zero", balance)
		return;
	}

	// add initial credit
	err = walletmanager.WmCredit(istore,ilog,"102","10000112.4401")
	if (err!="OK") {
		t.Errorf("walletmanager.WmCredit return error %s", err)
	}

	// add debits
	for i:=len(testAmounts)-1;i<=0;i++ {
		// test credit
		err := walletmanager.WmDebit(istore,ilog,"102",testAmounts[i])
		if ((err!="OK")&&(testAmounts[i]!="-100")){
			t.Errorf("walletmanager.WmDebit return error %s", err)
		}
		if (testAmounts[i]=="-100"){
			if (err !="Invalid negative credit") {
				t.Errorf("walletmanager.WmDebit return unexpected error for negative debit %s", err)
			}
		}
		balance,err := walletmanager.WmGetBalance(istore,ilog,"102")		
		if (err!="OK") {
			t.Errorf("walletmanager.WmGetBalance return error %s", err)
		}
		if (balance!=expectedAmounts[i]) {
			t.Errorf("walletmanager.WmGetBalance return unespected value %s instead of 100", balance)
		}
	}
}
