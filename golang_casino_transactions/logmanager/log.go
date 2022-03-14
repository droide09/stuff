package logmanager

import (
  "os"
  "github.com/sirupsen/logrus"
)

//
// DefaultLog
//
// write log using logrus library
//

type DefaultLog struct {
}

var log = logrus.New()

//
//  WriteLog
//
//  log generic info
//  step is a progressive number to indicate the position of the log in the function
//
func (l DefaultLog)WriteLog(functionName string,step int,str string){
	log.Out = os.Stdout
	log.WithFields(logrus.Fields{
    "module": functionName,
    "step":   step,
  }).Info(str)
}

func (l DefaultLog)WriteLogError(functionName string,step int,str string){
	log.Out = os.Stdout
	log.WithFields(logrus.Fields{
    "module": functionName,
    "step":   step,
  }).Error(str)
}

//
//  WriteLogReturn
//
//  log the return of a function
//  last parameter is the errorstring, "OK" is no errors
//
func (l DefaultLog)WriteLogReturn(module string,step int,str string,err string){
	log.Out = os.Stdout
	if (err=="OK"){
		log.WithFields(logrus.Fields{
			"module": module,
			"step":   step,
		}).Info(str)
	} else {
		log.WithFields(logrus.Fields{
			"module": module,
			"step":   step,
		}).Error(str + " "+ err)
	}	
}
