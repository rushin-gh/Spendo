import { useEffect, useState } from "react";
import { SPENDO_APP_BASE_URL } from "../config.js";

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
    console.log(expenses);
  }, []);

  const GetAllExpenses = async () => {
    const url = `${SPENDO_APP_BASE_URL}/api/expense/get`;

    try {
      const response = await fetch(url, {
        method: "GET",
        headers: {
          "Content-Type": "application-json",
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

  if (expenses == []) {
    return <div>Expenses are not loaded at.</div>;
  }

  return (
    <div>
      <table>
        <thead>
          <tr>
            <th>Sr</th>
            <th>Title</th>
            <th>Desc</th>
            <th>Amount</th>
          </tr>
        </thead>
        <tbody>
          <tr>
            <td>1</td>
            <td>Help</td>
            <td>No Help</td>
            <td>100</td>
          </tr>
        </tbody>
      </table>
    </div>
  );
};

export default Body;
