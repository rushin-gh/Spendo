import { useEffect, useState } from "react";
import { SPENDO_APP_BASE_URL } from "../../config.js";
import ExpenseAdd from "./expenseAdd.js";

const Body = () => {
  const [expenses, setExpenses] = useState([]);

  useEffect(() => {
    const loadExpenses = async () => {
      try {
        const expenses = await GetAllExpenses();
        setExpenses(expenses);
      } catch (err) {}
    };

    loadExpenses();
  }, []);

  const GetAllExpenses = async () => {
    const url = `${SPENDO_APP_BASE_URL}/api/expense/get`;

    try {
      const response = await fetch(url, {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
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

  if (expenses == null) {
    return <div id="body">Error while loading expenses</div>;
  } else if (expenses?.length == 0) {
    return <div id="body">No expenses</div>;
  } else {
    return (
      <div id="body">
        <ExpenseAdd />
        <table>
          <thead>
            <tr>
              <th>Sr</th>
              <th>Title</th>
              <th>Desc</th>
              <th>Amount</th>
              <th>Update</th>
              <th>Delete</th>
            </tr>
          </thead>
          <tbody>
            {expenses.map((exp) => {
              return (
                <tr key={exp.id}>
                  <td>{exp.id}</td>
                  <td>{exp.title}</td>
                  <td>{exp.description}</td>
                  <td>{exp.amount}</td>
                  <td>U</td>
                  <td>D</td>
                </tr>
              );
            })}
          </tbody>
        </table>
      </div>
    );
  }
};

export default Body;
