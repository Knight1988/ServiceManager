export default class ErrorCodes {
  static None = 0;
  static InternalServerError = 500;
  static NotImplemented = 501;
  // system wise error codes start with 1000
  static DataExist = 1000;
  // use error codes start with 2000
  static UserAndPasswordNotMatch = 2000;
}
