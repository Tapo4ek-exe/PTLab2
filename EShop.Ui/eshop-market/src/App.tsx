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
  Toaster,
  ToastIntent,
} from "@fluentui/react-components";
import { Products } from "./components/Products/Products";
import { Login } from "./components/Login/Login";
import { EShopApi } from "./api/EShopApi";
import { Profile } from "./components/Profile/Profile";
import { Product } from "./models/Product";
import { Purchase } from "./models/Purchase";
import { PurchaseList } from "./components/PurchaseList/PurchaseList";

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
    display: "flex",
    flexDirection: "row",
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
  const notify = (title: string, message: string, type: ToastIntent) =>
    dispatchToast(
      <Toast>
        <ToastTitle>{title}</ToastTitle>
        <ToastBody>{message}</ToastBody>
      </Toast>,
      { intent: type }
    );

  const [userIsLoggedIn, setUserIsLoggedIn] = useState(
    api.checkUserIsLoggedIn()
  );
  const [productList, setpProductList] = useState<Product[]>([]);
  const [purchaseList, setpPurchaseList] = useState<Purchase[]>([]);

  const getProducts = async () => {
    const response = await api.getProducts();
    if (response.IsSuccess && response.Data) {
      setpProductList(response.Data.products);
    } else {
      console.log(response.Errors);
      notify("Список товаров", "Ошибка при получении списка товаров!", "error");
    }
  };

  const getPurchases = async () => {
    const response = await api.getPurchases();
    if (response.IsSuccess && response.Data) {
      setpPurchaseList(response.Data.purchases);
    } else {
      console.log(response.Errors);
      notify("Список заказов", "Ошибка при получении списка заказов!", "error");
    }
  };

  const loadData = () => {
    getProducts();
    getPurchases();
  };

  useEffect(() => {
    loadData();
  }, [userIsLoggedIn]);

  return (
    <div className={classes.app}>
      <div className={classes.appHeader}>
        <div className={classes.logo}>
          <Title1>EShop</Title1>
        </div>
        <div className={classes.profile}>
          {userIsLoggedIn && <PurchaseList purchaseList={purchaseList} />}
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
        <Products
          products={productList}
          onPurchaseCreated={() => {
            notify("Покупка", "Заказ оформлен успешно!", "success");
            loadData();
          }}
          onError={(error) => {
            notify("Покупка", error, "error");
            loadData();
          }}
        />
      </div>
      <Toaster toasterId={toasterId} />
    </div>
  );
};

export default App;
