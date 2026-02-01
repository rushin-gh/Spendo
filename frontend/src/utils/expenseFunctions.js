import { URLs, HttpMethods, ContentTypes } from "./constants";

// Generic API request handler
const apiRequest = async (url, method, body = null) => {
  try {
    const config = {
      method,
      headers: {
        "Content-Type": ContentTypes.applicationJson,
      },
    };

    if (body) {
      config.body = JSON.stringify(body);
    }

    const response = await fetch(url, config);

    if (!response.ok) {
      throw new Error(`API Error: ${response.status} - ${response.statusText}`);
    }

    return await response.json();
  } catch (error) {
    console.error(`API Request failed: ${method} ${url}`, error);
    throw error;
  }
};

const GetAllExpenses = async () => {
  return await apiRequest(URLs.Expense.GetAllExpenses, HttpMethods.get);
};

const AddExpense = async (expense) => {
  const expenseData = {
    title: expense.title,
    description: expense.description,
    amount: expense.amount,
  };

  return await apiRequest(URLs.Expense.AddExpense, HttpMethods.post, expenseData);
};

const UpdateExpense = async (expenseId, expense) => {
  const expenseData = {
    title: expense.title,
    description: expense.description,
    amount: expense.amount,
  };

  const url = `${URLs.Expense.UpdateExpense}/${expenseId}`;
  return await apiRequest(url, HttpMethods.patch, expenseData);
};

const DeleteExpense = async (expenseId) => {
  const url = `${URLs.Expense.DeleteExpense}/${expenseId}`;
  return await apiRequest(url, HttpMethods.post);
};

export { GetAllExpenses, AddExpense, UpdateExpense, DeleteExpense };
