import React, { useState } from "react";
import {
  Dialog,
  DialogTrigger,
  DialogSurface,
  DialogTitle,
  DialogBody,
  DialogActions,
  DialogContent,
  Button,
  Field,
  Input,
  Body1Strong,
} from "@fluentui/react-components";
import { EShopApi } from "../../api/EShopApi";
import { Product } from "../../models/Product";

type PurchaseProps = {
  product: Product;
  onPurchaseCreated?: () => void;
  onError?: (error: string) => void;
};

const Purchase: React.FunctionComponent<PurchaseProps> = ({
  product,
  onPurchaseCreated,
  onError,
}) => {
  const [modalIsOpen, setModalIsOpen] = useState(false);
  const [address, setAddress] = useState("");
  const api = new EShopApi();

  const showModal = () => {
    setModalIsOpen(true);
  };

  const hideModal = () => {
    setModalIsOpen(false);
  };

  const makePurchase = async () => {
    const response = await api.makePurchase(product.id, address);
    if (response.IsSuccess) {
      hideModal();
      if (onPurchaseCreated) {
        onPurchaseCreated();
      }
    } else {
      console.log(response.Errors);
      if (onError) {
        onError("Ошибка проведения покупки!");
      }
    }
  };

  return (
    <Dialog open={modalIsOpen}>
      <DialogTrigger disableButtonEnhancement>
        <Button appearance="primary" onClick={showModal}>
          Купить
        </Button>
      </DialogTrigger>
      <DialogSurface>
        <DialogBody>
          <DialogTitle>Подтвердите покупку</DialogTitle>
          <DialogContent>
            <div style={{ display: "flex", flexDirection: "column" }}>
              <Body1Strong>Товар: {product.name}</Body1Strong>
              <Body1Strong>Стоимость: {product.salePrice}</Body1Strong>
            </div>
            <Field label="Адрес" required>
              <Input
                onChange={(ev, data) => {
                  setAddress(data.value);
                }}
              />
            </Field>
          </DialogContent>
          <DialogActions>
            <DialogTrigger disableButtonEnhancement>
              <Button appearance="secondary" onClick={hideModal}>
                Отмена
              </Button>
            </DialogTrigger>
            <Button
              appearance="primary"
              onClick={() => {
                if (address.length < 1 && onError) {
                  onError("Заполните адрес!");
                } else {
                  makePurchase();
                }
              }}
            >
              Подтвердить
            </Button>
          </DialogActions>
        </DialogBody>
      </DialogSurface>
    </Dialog>
  );
};

export { Purchase };
