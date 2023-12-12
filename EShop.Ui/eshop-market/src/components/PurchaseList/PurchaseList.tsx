import {
  Card,
  DataGrid,
  DataGridHeader,
  DataGridRow,
  DataGridHeaderCell,
  DataGridBody,
  DataGridCell,
  Body1Strong,
  TableColumnDefinition,
  createTableColumn,
  Button,
  Dialog,
  DialogActions,
  DialogBody,
  DialogContent,
  DialogSurface,
  DialogTitle,
  DialogTrigger,
} from "@fluentui/react-components";
import React, { useState } from "react";
import { Product } from "../../models/Product";
import { Purchase } from "../../models/Purchase";

type PurchaseListProps = {
  purchaseList: Purchase[];
};

const PurchaseList: React.FunctionComponent<PurchaseListProps> = ({
  purchaseList,
}) => {
  const [modalIsOpen, setModalIsOpen] = useState(false);

  const columns: TableColumnDefinition<Purchase>[] = [
    createTableColumn<Purchase>({
      columnId: "purchaseName",
      renderHeaderCell: () => {
        return "Название товара";
      },
      renderCell: (item) => {
        return item.product.name;
      },
    }),
    createTableColumn<Purchase>({
      columnId: "productPrice",
      renderHeaderCell: () => {
        return "Цена товара";
      },
      renderCell: (item) => {
        return item.usedPrice;
      },
    }),
    createTableColumn<Purchase>({
      columnId: "purchaseAddress",
      renderHeaderCell: () => {
        return "Адреса";
      },
      renderCell: (item) => {
        return item.address;
      },
    }),
    createTableColumn<Purchase>({
      columnId: "purchaseDate",
      renderHeaderCell: () => {
        return "Дата";
      },
      renderCell: (item) => {
        return item.date;
      },
    }),
  ];

  const showModal = () => {
    setModalIsOpen(true);
  };

  const hideModal = () => {
    setModalIsOpen(false);
  };

  return (
    <Dialog open={modalIsOpen}>
      <DialogTrigger disableButtonEnhancement>
        <Button appearance="transparent" onClick={showModal}>
          Заказы
        </Button>
      </DialogTrigger>
      <DialogSurface>
        <DialogBody>
          <DialogTitle>Списко заказов</DialogTitle>
          <DialogContent>
            <Card>
              <DataGrid
                items={purchaseList}
                columns={columns}
                getRowId={(item) => item.id}
              >
                <DataGridHeader>
                  <DataGridRow>
                    {({ renderHeaderCell }) => (
                      <DataGridHeaderCell>
                        {renderHeaderCell()}
                      </DataGridHeaderCell>
                    )}
                  </DataGridRow>
                </DataGridHeader>
                <DataGridBody<Product>>
                  {({ item, rowId }) => (
                    <DataGridRow<Product> key={rowId}>
                      {({ renderCell }) => (
                        <DataGridCell>{renderCell(item)}</DataGridCell>
                      )}
                    </DataGridRow>
                  )}
                </DataGridBody>
              </DataGrid>
            </Card>
          </DialogContent>
          <DialogActions>
            <DialogTrigger disableButtonEnhancement>
              <Button appearance="secondary" onClick={hideModal}>
                Назад
              </Button>
            </DialogTrigger>
          </DialogActions>
        </DialogBody>
      </DialogSurface>
    </Dialog>
  );
};

export { PurchaseList };
