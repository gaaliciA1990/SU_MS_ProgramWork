
import org.openqa.selenium.*;
import org.openqa.selenium.chrome.*;
import org.junit.jupiter.api.Test;
import org.openqa.selenium.support.ui.ExpectedConditions;
import org.openqa.selenium.support.ui.WebDriverWait;

import org.apache.commons.io.FileUtils;
import java.io.File;
import java.time.Duration;

import static org.junit.jupiter.api.Assertions.*;

public class WebDriver_Tests {
    public static void screenShot(WebDriver webdriver, String fileWithPath) throws Exception{

        //Convert web driver object to TakeScreenshot
        TakesScreenshot sourcePhoto = ((TakesScreenshot)webdriver);

        //Call getScreenshotAs method to create image file
        File sourceFile = sourcePhoto.getScreenshotAs(OutputType.FILE);

        //Move image file to new destination
        File destination = new File(fileWithPath);

        //Copy file at destination
        FileUtils.copyFile(sourceFile, destination);

    }
    @Test
    public void testAmazonAddToCart() throws Exception {
        // Optional. If not specified, WebDriver searches the PATH for chromedriver.
        System.setProperty("webdriver.chrome.driver", "C:\\Users\\garci\\chromedriver\\chromedriver.exe");
        WebDriver driver = new ChromeDriver();
        String filePath = "C:\\Users\\garci\\Documents\\SUMSCS\\Testing_Debugging\\";

        // Navigate to the amazon site
        driver.get("http://www.amazon.com/");
        screenShot(driver, filePath + "amazonSite.png"); // take a screenshot
        Thread.sleep(2000);  // wait 2 seconds, Let the user actually see something!

        // search for the dewalt item
        WebElement searchBox = driver.findElement(By.id("twotabsearchtextbox"));
        searchBox.sendKeys("Dewalt oscillating 20V Max");
        searchBox.submit();
        screenShot(driver, filePath + "searchbox.png");
        Thread.sleep(2000); // wait 2 seconds, Let the user actually see something!

        // click on the first item in the list
        WebElement selectItem = driver.findElement(By.className("s-asin"));
        selectItem.click();
        screenShot(driver, filePath + "clickItem.png");
        Thread.sleep(2000); // wait 2 seconds, Let the user actually see something!

        // click on the add to cart button
        WebElement addToCart = driver.findElement(By.id("add-to-cart-button"));
        addToCart.click();
        screenShot(driver, filePath + "addToCart.png");
        Thread.sleep(2000); // wait 2 seconds, Let the user actually see something!

        // decline coverage, but wait for the button to be clickable (10 secs)
        Duration time = Duration.ofSeconds(10, 1);
        WebDriverWait wait = new WebDriverWait(driver, time);
        WebElement declineCoverage = driver.findElement(By.id("attachSiNoCoverage"));
        wait.until(ExpectedConditions.elementToBeClickable(declineCoverage)).click();
        screenShot(driver, filePath + "declineCoverage.png");
        Thread.sleep(2000); // wait 2 seconds, Let the user actually see something!

        // validate our cart now has 1 item added
        WebElement cartCount = driver.findElement(By.id("nav-cart-count"));
        String expectedCount = "1";
        assertEquals(expectedCount, cartCount.getText());
        screenShot(driver, filePath + "validateCart.png");
        Thread.sleep(2000); // wait 2 seconds, Let the user actually see something!

        driver.quit();
    }

    @Test
    public void testSmileAmazon_SignInStatus() throws Exception {
        // Optional. If not specified, WebDriver searches the PATH for chromedriver.
        System.setProperty("webdriver.chrome.driver", "C:\\Users\\garci\\chromedriver\\chromedriver.exe");
        WebDriver driver = new ChromeDriver();
        String filePath = "C:\\Users\\garci\\Documents\\SUMSCS\\Testing_Debugging\\";


        driver.get("http://smile.amazon.com/");
        screenShot(driver, filePath + "smiLeAmazon.png");
        Thread.sleep(2000);  // wait 5 seconds, Let the user actually see something!

        WebElement signInVerification = driver.findElement(By.linkText("Sign in"));
        if ( signInVerification == null) {
            throw new InterruptedException("Already signed into smile.amazon.com");
        }
        screenShot(driver, filePath + "smiLeAmazon_SignIn.png");
        Thread.sleep(2000);  // wait 5 seconds, Let the user actually see something!


        WebElement learnMore = driver.findElement(By.linkText("Learn more about AmazonSmile"));
        learnMore.click();
        screenShot(driver, filePath + "learnMore.png");
        Thread.sleep(2000); // wait 5 seconds, Let the user actually see something!

        driver.quit();
    }
}
