import { SPENDO_APP_BASE_URL } from "../config.js";

const Body = () => {
  console.log(SPENDO_APP_BASE_URL);

  return (
    <div>
      {SPENDO_APP_BASE_URL}
      {/* <table></table> */}
    </div>
  );
};

export default Body;
