import { SPENDO_APP_BASE_URL } from "../config";

const URLs = {
  Expense: {
    GetAllExpenses: `${SPENDO_APP_BASE_URL}/api/Expense/get`,
    AddExpense: `${SPENDO_APP_BASE_URL}/api/expense/add`,
    UpdateExpense: `${SPENDO_APP_BASE_URL}/api/expense/update`, // {BASE_URL}/api/expense/update/{expId} - Do not forget to append expId as path param
    DeleteExpense: `${SPENDO_APP_BASE_URL}/api/expense/delete`, // {BASE_URL}/api/expense/delete/{expId} - - Do not forget to append expId as path param
  },
};

const HttpMethods = {
  get: "GET",
  post: "POST",
  put: "PUT",
  patch: "PATCH",
  delete: "DELETE",
};

const ContentTypes = {
  applicationJson: "application/json",
};

export { URLs, HttpMethods, ContentTypes };
