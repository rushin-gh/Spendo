import { URLs, HttpMethods, ContentTypes } from "./constants";

const GetAllExpenses = async () => {
  const url = URLs.Expense.GetAllExpenses;

  try {
    const response = await fetch(url, {
      method: HttpMethods.get,
      headers: {
        "Content-Type": ContentTypes.applicationJson,
      },
    });

    if (!response.ok) {
      throw new Error("Api thrown some error.");
    }

    const data = await response.json();
    return data;
  } catch (err) {
    console.log("Error");
  }
};

const AddExpense = (exp) => {
  let expense = {
    title: exp.title,
    description: exp.description,
    amount: exp.amount,
  };

  const url = URLs.Expense.AddExpense;
  try {
    const response = fetch(url, {
      method: HttpMethods.post,
      headers: {
        "Content-Type": ContentTypes.applicationJson,
      },
      body: JSON.stringify(expense),
    });

    if (!response.ok) {
      throw new Error("Api thrown some error.");
    }
  } catch (error) {
    console.log(error.Message);
  }
};

const UpdateExpense = (expId, exp) => {
  let expense = {};
  if (exp.title != "") expense.title = exp.title;
  if (exp.description != "") expense.description = exp.description;
  if (exp.amount != "") expense.amount = exp.amount;

  const url = `${URLs.Expense.DeleteExpense}/${expId}`;
  try {
    const response = fetch(url, {
      method: HttpMethods.patch,
      headers: {
        "Content-Type": ContentTypes.applicationJson,
      },
      body: JSON.stringify(expense),
    });

    if (!response.ok) {
      throw new Error("Api thrown some error.");
    }
  } catch (error) {
    console.log(error.Message);
  }
};

const DeleteExpense = async (expId) => {
  const url = `${URLs.Expense.DeleteExpense}/${expId}`;

  try {
    const response = await fetch(url, {
      method: HttpMethods.post,
      headers: {
        "Content-Type": ContentTypes.applicationJson,
      },
    });

    if (!response.ok) {
      throw new Error("Api thrown some error.");
    }

    const data = await response.json();
    loadExpenses();
    return data;
  } catch (err) {
    console.log("Error");
  }
};

export { GetAllExpenses, AddExpense, UpdateExpense, DeleteExpense };
