import React, { useEffect, useState } from "react";
import {
  Divider,
  Link,
  makeStyles,
  Title1,
  Title3,
  Toast,
  ToastBody,
  ToastFooter,
  ToastTitle,
  useToastController,
  useId,
  Toaster
} from "@fluentui/react-components";
import { Products } from "./components/Products/Products";
import { Login } from "./components/Login/Login";
import { EShopApi } from "./api/EShopApi";
import { Profile } from "./components/Profile/Profile";
import { Product } from "./models/Product";

const useStyles = makeStyles({
  app: {
    display: "flex",
    flexDirection: "column",
    justifyContent: "center",
    maxWidth: "1200px",
    marginLeft: "auto",
    marginRight: "auto",
    marginTop: "auto",
    marginBottom: "auto",
  },
  appHeader: {
    display: "flex",
    flexDirection: "row",
    width: "100%",
    marginBottom: "10px",
  },
  logo: {
    marginLeft: "10px",
    marginRight: "auto",
    marginTop: "auto",
    marginBottom: "auto",
  },
  profile: {
    marginRight: "10px",
    marginLeft: "auto",
    marginTop: "auto",
    marginBottom: "auto",
  },
  appContainer: {
    display: "flex",
    flexDirection: "column",
  },
});

const App: React.FunctionComponent = () => {
  const classes = useStyles();
  const api = new EShopApi();

  const toasterId = useId("toaster");
  const { dispatchToast } = useToastController(toasterId);
  const notifyError = () =>
    dispatchToast(
      <Toast>
        <ToastTitle>Список товаров</ToastTitle>
        <ToastBody>Ошибка при получении списка товаров!</ToastBody>
      </Toast>,
      { intent: "error" }
    );

  const [userIsLoggedIn, setUserIsLoggedIn] = useState(
    api.checkUserIsLoggedIn()
  );
  const [productList, setpProductList] = useState<Product[]>([]);

  const getProducts = async () => {
    const response = await api.getProducts();
    console.log(response);
    if (response.IsSuccess) {
      setpProductList(response.Data ?? []);
    } else {
      console.log(response.Errors);
      notifyError();
    }
  };

  useEffect(() => {
    getProducts();
  }, [userIsLoggedIn]);

  return (
    <div className={classes.app}>
      <div className={classes.appHeader}>
        <div className={classes.logo}>
          <Title1>EShop</Title1>
        </div>
        <div className={classes.profile}>
          {userIsLoggedIn ? (
            <Profile
              onLogout={() => {
                setUserIsLoggedIn(false);
              }}
            />
          ) : (
            <Login
              onLoginSuccess={() => {
                setUserIsLoggedIn(true);
              }}
            />
          )}
        </div>
      </div>
      <Divider />
      <div className={classes.appContainer}>
        <Title3>Товары:</Title3>
        <Products products={productList} />
      </div>
      <Toaster toasterId={toasterId} />
    </div>
  );
};

export default App;
