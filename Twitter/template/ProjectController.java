import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.application.Platform;
import javafx.scene.control.TextArea;
import javax.swing.*;
import java.io.FileNotFoundException;

public class ProjectController
{
    String hight, width;
    @FXML
    private TextArea starsArea;

    public void initialize() throws FileNotFoundException
    {


    }


    @FXML
    void btnExit(ActionEvent event)
    {
        Platform.exit();
    }

    @FXML
    void btnRec(ActionEvent event)
    {
        starsArea.clear();
        hight = JOptionPane.showInputDialog("Enter Hight of the tower");
        int rec_hight = Integer.parseInt(hight);
        width = JOptionPane.showInputDialog("Enter width of the tower");
        int rec_width = Integer.parseInt(width);
        if (rec_hight == rec_width || Math. abs(rec_hight-rec_width)>5)
            JOptionPane.showMessageDialog(null, "Area:" + rec_hight * rec_width);
        else
            JOptionPane.showMessageDialog(null, "Perimeter:" + 2*(rec_hight+rec_width));
    }

    @FXML
    void btnTriangle(ActionEvent event)
    {
        starsArea.clear();
        hight = JOptionPane.showInputDialog("Enter Hight of the tower");
        int trng_hight = Integer.parseInt(hight);
        width = JOptionPane.showInputDialog("Enter width of the tower");
        int trng_width = Integer.parseInt(width);
        String ans = JOptionPane.showInputDialog("To calculate the perimeter of the triangle, insert 1, to print the triangle insert 2");
        int ansNum = Integer.parseInt(ans);
        if (ansNum == 1)
        {
            double permiter =trng_width + 2*Math.sqrt((trng_width/2)^2 + trng_hight^2) + trng_width;
            JOptionPane.showMessageDialog(null, "Permiter:" + permiter);
        }
        else
        {
            String trng = "";
            int numAsteriskRowsMiddle = (trng_hight-2)/((trng_width-3)/2);
            int remain = (trng_hight-2)%((trng_width-3)/2);
            int numOfSpaces = (trng_width-1)/2;
            // add the first line
            for (int l = 0; l < numOfSpaces; l++)
            {
                trng+=" ";
            }
            trng +="*";
            trng += "\n";
            //add the middle lines
            for(int i = 1; i<=(trng_width-3)/2 ; i++)
            {
                numOfSpaces--;
                if(i ==1)
                    for(int j=0; j<remain; j++)
                    {
                        for (int l = 0; l < numOfSpaces; l++)
                        {
                            trng+=" ";
                        }
                        for(int k=0; k<i+(i-1)+2; k++)
                        {
                            trng+="*";
                        }
                        trng+="\n";

                    }
                for(int j=0; j<numAsteriskRowsMiddle; j++)
                {
                    for (int l = 0; l < numOfSpaces; l++)
                    {
                        trng+=" ";
                    }
                    for(int k=0; k<i+(i-1)+2; k++)
                    {
                        trng+="*";
                    }

                    trng+="\n";
                }
            }
            //add the last line
            for (int i = 0; i < trng_width; i++)
            {
                trng+="*";
            }
            starsArea.setStyle("-fx-text-fill: blue;");
            starsArea.setText(trng);
        }

    }


}

