import java.io.Serializable;
import java.util.Objects;

public abstract class ValueObject<T> implements Serializable {
    public T value;

    public ValueObject(T value) {
        this.value = value;
    }

    public T value() {
        return value;
    }
    
    @Override
    public boolean equals(Object o) {
        if (this == o) {
            return true;
        }
        if (o == null || getClass() != o.getClass()) {
            return false;
        }
        ValueObject n = (ValueObject) o;
        
        return this.value.equals(n.value);
    }
    
    @Override
    public int hashCode() {
        return Objects.hash(this.value);
    }
}

public final class UserFirstName extends ValueObject<String> {
    public static final int MIN_LENGTH = 3;
    public static final int MAX_LENGTH = 40;

    public UserFirstName(String value) throws InvalidAttributeException {
        super(value);

        if (value.length() < UserFirstName.MIN_LENGTH) {
            throw InvalidAttributeException.fromMinLength("first name", UserFirstName.MIN_LENGTH);
        }

        if (value.length() > UserFirstName.MAX_LENGTH) {
            throw InvalidAttributeException.fromMinLength("first name", UserFirstName.MAX_LENGTH);
        }
    }

    public UserFirstName(){
        super("");
    }
}