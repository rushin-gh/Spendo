import React from "react";
import ReactDOM from "react-dom/client";
import Body from "./components/body";

const App = () => {
  return (
    <div>
      <div>Header</div>
      <Body />
      <div>Footer</div>
    </div>
  );
};

let root = ReactDOM.createRoot(document.getElementById("root"));
root.render(<App />);
