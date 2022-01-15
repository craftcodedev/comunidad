abstract class ValueObject<T> {
  constructor(protected props: T) {
    const baseProps: any = {
      ...props,
    };

    this.props = baseProps;
  }

  public equals(vo?: ValueObject<T>): boolean {
    if (vo === null || vo === undefined) {
      return false;
    }
    if (vo.props === undefined) {
      return false;
    }

    return JSON.stringify(this.props) === JSON.stringify(vo.props);
  }

  public value(): T {
    return this.props;
  }
}

class UserFirstName extends ValueObject<String> {
  public static readonly MIN_LENGTH = 3;
  public static readonly MAX_LENGTH = 40;

  private constructor(value: string) {
    if (value.length < UserFirstName.MIN_LENGTH) {
      throw InvalidAttributeException.FromMinLength(
        "first name",
        UserFirstName.MIN_LENGTH
      );
    }

    if (value.length > UserFirstName.MAX_LENGTH) {
      throw InvalidAttributeException.FromMaxLength(
        "first name",
        UserFirstName.MAX_LENGTH
      );
    }

    super(value);
  }
}
